using System;
using System.IO;
using System.Reflection;
using Demo.Core.Handle;
using Demo.Data;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Demo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddDbContext<DemoDbContext>(options =>
            {
#if DEBUG
                options.EnableSensitiveDataLogging(true);
#endif
                options.UseMySql(Configuration.GetConnectionString("DBConnection"));
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://demo.identityserver.io";
                    options.ApiName = "api1";
                });

            services.AddTransient<ExceptionFilter>();

            services.AddControllers(options =>
            {
                options.Filters.Add(new ModelActionFilter());
                //options.Filters.AddService<ExceptionFilter>();
                options.MaxModelValidationErrors = 50;
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    _ => "���ֶβ���Ϊ�ա�");
            });
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            #region Cors

            if (!Environment.IsProduction())
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });
            }
            else
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.WithOrigins("https://localhost:1000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });
            }

            #endregion

            #region Swagger
            //ע��Swagger������������һ���Ͷ��Swagger �ĵ�
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Breath.Monitor.API",
                    Version = "v1",
                });

                // ����SWAGER JSON��UI��ע��·����
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Demo.Core.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Demo.Data.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

                // enable swagger Annotations
                options.EnableAnnotations();
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/api1");

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            #region Swagger
            //�����м����������Swagger��ΪJSON�ս��
            app.UseSwagger();
            //�����м�������swagger-ui��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "ȫ�� API");
                options.RoutePrefix = "swagger";//·�����ã�����Ϊ�գ���ʾֱ�ӷ��ʸ��ļ���
                //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�
                //���ʱ��ȥlaunchSettings.json�а�"launchUrl": "swagger/index.html"ȥ���� Ȼ��ֱ�ӷ���localhost:8001/index.html����
            });
            #endregion

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    "default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
