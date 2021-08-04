using CoreServices.Models;
using CoreServices.Repository;
using CoreServices.Repository.CoreServices.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreServices
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
            services.AddCors(option => option.AddPolicy("MyBlogPolicy", builder => {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

            }));
            services.AddControllers();

            //CONTEXT STRING
            services.AddDbContext<BlogDBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("BlogDBConnection")));
            
            //INJEÇÃO DE CLASSE NO STARTUP
            services.AddScoped<IPostRepository, PostRepository>();

            //REGISTRA O GERADOR SWAGGER DEFININDO UM OU MAIS DOCUMENTOS SWAGGER
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
                    
                    Title = "APIBlog", 
                    Version = "v1",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact{ Name="Lucas Ferreira", Email="lucasferreirabatista5@gmail.com" }
                
                });

            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("MyBlogPolicy");

            app.UseRouting();

            app.UseAuthorization();

            //HABILITARA O MIDDLEWARE PARA SERVIR O SWAGGER GERADO COM UM ENDPOINT JSON
            app.UseSwagger();

            //HABILITARA O MIDDLEWARE PARA SERVIR O SWAGGER-UI (HTML, JS, CSS, ETC)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIBlog v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
