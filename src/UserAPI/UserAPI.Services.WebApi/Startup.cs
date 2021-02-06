using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO.Compression;
using System.Linq;
using UserAPI.Infra.CrossCutting.IoC;
using UserAPI.Services.WebApi.Configurations;
using UserAPI.Services.WebApi.Middlewares;

namespace UserAPI.Services.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            })
            .AddXmlSerializerFormatters()
            .AddXmlDataContractSerializerFormatters();

            //API Versioning
            services.AddVersioningConfiguration();

            //JWT Auth
            services.AddJwtConfiguration(Configuration.GetValue<string>("SecretKey"));

            //Compression
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

            //check compression on network tab to see uncompressed versus compressed request size
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" }); //compress only json requests
                options.EnableForHttps = true;
            });

            //EF In Memory
            services.AddInMemoryDatabaseConfiguration("MemoryDatabase");

            //AutoMapper
            services.AddAutoMapperConfiguration();

            //IoC 
            services.RegisterServices();

            //Api Docs
            services.AddSwaggerConfiguration();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserAPI v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseCustomResponseFormatter();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
