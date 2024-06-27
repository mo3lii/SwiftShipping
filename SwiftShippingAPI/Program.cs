using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(o =>
{
    o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("mainString"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    options => { options.User.AllowedUserNameCharacters = null; }
    )
    .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = null; // Example option, adjust as needed
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





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
