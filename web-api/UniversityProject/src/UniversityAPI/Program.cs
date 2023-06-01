using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using UniversityAPI;
using UniversityAPI.Extensions;
using UniversityAPI.Services;
using UniversityAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.BindConfigurations(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddMsSqlServer(builder.Configuration);
builder.Services.AddJwtBearerAuthentication(builder.Configuration);
builder.Services.AddHostedService<TokenCleanerBackgroundService>();

builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IBrandsService, BrandsService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IImageService, ImageService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(app.Environment.WebRootPath),
    RequestPath = "/images"
});
app.UseAuthentication();
app.UseAuthorization();

// app.MapControllers();
app.MapControllers().RequireAuthorization();

app.Run();