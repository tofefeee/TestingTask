using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using TradeArtTest.Data;
using TradeArtTest.Interfaces;

namespace TradeArtTest
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
      services.Configure<Settings>(Configuration.GetSection("Settings"));

      services.AddControllers();
      services.AddControllers().AddJsonOptions(o => 
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
      services.AddScoped<InvertStringRepo, InvertStringRepo>();
      services.AddScoped<ILoopsAndThreadsRepo, LoopsAndThreadsRepo>();
      services.AddScoped<ICalculateHashRepo, CalculateHashRepo>();
      services.AddScoped<IFetchAssetsRepo, FetchAssestRepo>();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "TradeArtTest", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TradeArtTest v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}