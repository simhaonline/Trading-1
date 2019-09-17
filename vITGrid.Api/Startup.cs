using System;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using vITGrid.Common.Log.Interfaces;
using vITGrid.Common.Log.Models;
using vITGrid.Common.Log.Types;

namespace vITGrid.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Configuration = configuration;

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);

            Configuration = configurationBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSingleton<ILog>(new Log.Log(
                Configuration.GetSection("LogSettings")["LogFilePath"],
                Configuration.GetSection("LogSettings")["LogFileName"],
                Enum.Parse<ErrorType>(Configuration.GetSection("LogSettings")["LogErrorLevel"]),
                long.Parse(Configuration.GetSection("LogSettings")["ArchiveFileSize"]),
                int.Parse(Configuration.GetSection("LogSettings")["ArchiveFileCount"])));

            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddCors();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            if(hostingEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }
            else
            {
                applicationBuilder.UseHsts();
            }

            applicationBuilder.UseResponseCompression();
            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseMvc();
        }
    }
}