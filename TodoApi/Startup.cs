using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Mmodels;

namespace TodoApi
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app)
        //{
        //    app.UseMvc(
        //    //    routes=>
        //    //{
        //    //    routes.MapRoute("default", "index.html");
        //    //}
        //    );
        //}

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Serve the files Default.htm, default.html, Index.htm, Index.html
            // by default (in this order), i.e., without having to explicitly qualify the URL.
            // For example, if your endpoint is http://localhost:3012/ and wwwroot directory
            // has Index.html, then Index.html will be served when someone hits
            // http://localhost:3012/
            app.UseDefaultFiles();

            // Enable static files to be served. This would allow html, images, etc. in wwwroot
            // directory to be served. 
            app.UseStaticFiles();

            app.UseMvc();

        }
    }
}
