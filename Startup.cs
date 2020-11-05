using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reddit.Database;
using Reddit.Services;
using Reddit.Services.UserServices;

namespace Reddit
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext applicationDbContext;

        public Startup(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            this.configuration = configuration;
            this.applicationDbContext = applicationDbContext;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<Database.ApplicationDbContext>(builder => builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            applicationDbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
