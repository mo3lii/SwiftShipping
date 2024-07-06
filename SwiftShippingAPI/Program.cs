using E_CommerceAPI.Errors;
using E_CommerceAPI.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.Helper;
using SwiftShipping.ServiceLayer.Services;
using SwiftShipping.API.Authorization;
using System.Text;
using SwiftShipping.DataAccessLayer.Enum;


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
    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
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
    options.AddPolicy("Employees/View", policy =>
             policy.RequireAuthenticatedUser()
                   .AddRequirements(new PermissionRequirement(Department.Employees, PermissionType.View)));

    //policy name : DepartmentName/Permission  example: Branch/View
    //AuthorizationPolicies.AddDepartmentsPolicies(options);
});


// Register services required for authorization
builder.Services.AddScoped<IRolePermissionsService, RolePermissionsService>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();

// Ensure RoleManager<IdentityRole> is registered if used
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddScoped<HttpContextAccessor>();

//classes registerations
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DeliveryManService>();
builder.Services.AddScoped<GovernmentService>();
builder.Services.AddScoped<RegionService>();
builder.Services.AddScoped<WeightSettingService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<BranchService>();
builder.Services.AddScoped<RolesService>();


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseMiddleware<ExceptionMeddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
