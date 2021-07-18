using Amazon.S3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoCaptain.Web.Data;
using PhotoCaptain.Web.Services;

namespace PhotoCaptain.Web {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) {
      services.AddControllersWithViews();

      services.AddDbContext<PhotoContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("PhotoContext")));

      services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
      services.AddAWSService<IAmazonS3>();
      services.Configure<S3Configuration>(Configuration.GetSection("S3"));
 
      services.AddSingleton<IPhotoRepository, S3PhotoRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      } else {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapControllerRoute(
          name: "photos",
          pattern: "Photos/{*uri}",
          defaults: new { controller = "Photos", action = "Show" });
      });

      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {
        var context = serviceScope.ServiceProvider.GetRequiredService<PhotoContext>();
        context.Database.EnsureCreated();
      }
    }
  }
}
