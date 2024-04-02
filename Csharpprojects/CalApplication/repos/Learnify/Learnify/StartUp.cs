﻿using Learnify.Data;
using Learnify.Interface;
using Learnify.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace Learnify
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Adding DbContext and connection string
            var connectionString = Configuration.GetConnectionString("myConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string is not defined or empty.");
            }
            services.AddDbContext<LearnifyContext>(options =>
                options.UseSqlServer(connectionString));

            // Adding JWT authentication
            var token = Configuration.GetSection("AppSettings:Token").Value;
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("JWT token is not defined or empty.");
            }
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token))
                };
            });

            // Adding Swagger documentation
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            // Adding application services
            services.AddTransient<LearnifyInterface, LearnifyRepo>();
            services.AddCors(options => {
                options.AddPolicy("AllowAnyOrigin", builder => {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Learnify V1");
                c.RoutePrefix = "swagger";
            });
            app.UseCors(
 builder => {
     builder.AllowAnyOrigin();
     builder.AllowAnyMethod();
     builder.AllowAnyHeader();
 });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
