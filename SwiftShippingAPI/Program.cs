using E_CommerceAPI.Errors;
using E_CommerceAPI.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Permissions;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.Helper;
using SwiftShipping.ServiceLayer.Services;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//Define string variable as Cors policy in start class 
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//Register AddCors in ConfigureServices method
builder.Services.AddCors(options => {
    options.AddPolicy(MyAllowSpecificOrigins, builder => {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(o =>
{
    o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("mainString"));
});
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    options => { options.User.AllowedUserNameCharacters = null; }
    )
    .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = null; // Example option, adjust as needed
});

builder.Services.AddAuthentication(
 option => option.DefaultAuthenticateScheme = "myscheme")
    .AddJwtBearer("myscheme",
   op =>
   {
       #region secret key
       string key = "welcome to my secret key Ahmed Mohamed Samir";
       var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
       #endregion
       op.TokenValidationParameters = new TokenValidationParameters()
       {
           IssuerSigningKey = secertkey,
           ValidateIssuer = false,
           ValidateAudience = false

       };
   });

//Configer Error message from our api 
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = ActionContext =>
    {
        var errors = ActionContext.ModelState.Where(error => error.Value.Errors.Count > 0)
        .SelectMany(value => value.Value.Errors)
        .Select(error => error.ErrorMessage).ToArray();

        var errorResponse = new ApiValidation
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanView", policy => policy.RequireClaim(Permissions.View));
    options.AddPolicy("CanEdit", policy => policy.RequireClaim(Permissions.Edit));
    options.AddPolicy("CanDelete", policy => policy.RequireClaim(Permissions.Delete));
    options.AddPolicy("CanAdd", policy => policy.RequireClaim(Permissions.Add));
});

//classes registerations
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DeliveryManService>();
builder.Services.AddScoped<GovernmentService>();
builder.Services.AddScoped<RegionService>();
builder.Services.AddScoped<WeightSettingService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<BranchService>();
builder.Services.AddScoped<RolePermissionService>();


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMeddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//Add UseCors Middleware in Configure method
app.UseCors(MyAllowSpecificOrigins);

app.Run();
