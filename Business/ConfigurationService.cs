using Business.Services.Abstract;
using Business.Services.Concrete;
using Business.Utilities.Profiles;
using Core.Entities.Concrete.Auth;
using DataAccess.EFCore;
using DataAccess.UnitOfWork.Abstract;
using DataAccess.UnitOfWork.Concrete;
using Entities.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Business
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddBusinessConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(CategoryProfile));
            services.AddFluentValidationAutoValidation()
              .AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddIdentity<AppUser, IdentityRole>()
                       .AddEntityFrameworkStores<AppDbContext>()
                       .AddDefaultTokenProviders();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                var tokenOption = configuration
                    .GetSection("TokenOptions")
                    .Get<TokenOption>();

                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,

                    ValidIssuer = tokenOption.Issuer,
                    ValidAudience = tokenOption.Audience,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(tokenOption.SecurityKey)
                    ),

                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });



            return services;
        }
    }
}

