using CRM_Application.MediatRDependencies;
using CRM_Infrastraction.Persistence;
using CRM_Infrastraction.Services;
using CRM_Interface.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;



var builder = WebApplication.CreateBuilder(args);


// MediatR CQRS
builder.Services.AddMediatRDependency(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddAuthentication(idx =>
{

        idx.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        idx.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JWT");
        options.RequireHttpsMetadata = false;
        options.SaveToken = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"])
            )
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpClient<IGroqAIService, GroqAIService>(client =>
{
    client.DefaultRequestHeaders.Add("Authorization", "Bearer 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");
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(idx =>
{
    idx.SwaggerDoc("v1", new OpenApiInfo() { Title = "Api V1", Version = "v1.0" });

    // إعداد الأمان باستخدام Bearer
    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
    };

    // إضافة تعريف الأمان
    idx.AddSecurityDefinition("Bearer", securitySchema);

    // إضافة متطلبات الأمان
    idx.AddSecurityRequirement(new OpenApiSecurityRequirement
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


builder.Services.AddCors(idx =>
{
    idx.AddPolicy("PolicyCors", c => {
        c.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
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

app.UseCors("PolicyCors");
app.UseAuthentication();
app.UseAuthorization();
await app.SeedIdentityDataAsync();

app.MapControllers();

app.Run();
