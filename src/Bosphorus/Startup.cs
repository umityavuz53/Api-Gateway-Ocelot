namespace Bosphorus
{
    using System;
    using System.Text;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;

    using Ocelot.DependencyInjection;
    using Ocelot.Middleware;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            SymmetricSecurityKey signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Security"]));

            string authenticationProviderKey = "TestKey";
            services.AddAuthentication()
                .AddJwtBearer(authenticationProviderKey, options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                                                                {
                                                                    ValidateIssuerSigningKey = true,
                                                                    IssuerSigningKey = signInKey,
                                                                    ValidateIssuer = true,
                                                                    ValidIssuer = _configuration["JWT:Issuer"],
                                                                    ValidateAudience = true,
                                                                    ValidAudience = _configuration["JWT:Audience"],
                                                                    ValidateLifetime = true,
                                                                    ClockSkew = TimeSpan.Zero,
                                                                    RequireExpirationTime = true
                                                                };
                    });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
