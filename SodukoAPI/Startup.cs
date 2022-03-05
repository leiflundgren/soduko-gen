namespace SodukoAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Services.Generator>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});


                // endpoints.MapDefaultControllerRoute();

                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Generate}/{reducers}/{percentExtra}/");
            });
        }
    }
}
