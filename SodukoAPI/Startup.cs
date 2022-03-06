namespace SodukoAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<Services.Generator>();
            services.AddControllersWithViews();
            services.AddSwaggerGen();
           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
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
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=GenerateObject}/{reducers}/{percentExtra}/");
            });


        }
    }
}
