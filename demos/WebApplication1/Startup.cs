using AspectCore.Extensions.DependencyInjection;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication1.Extend;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // ��asp.net core��Ŀ�У����Խ�����asp.net core������ע�룬�򻯴���������ע�룬
            // �������Լ����� ProxyGeneratorBuilder ���д���������ע���ˡ�
            //services.AddScoped<Person>();
            RegisterServices(this.GetType().Assembly, services);
            // Install-Package AspectCore.Extensions.DependencyInjection
            // ��aspectcore�ӹ�ע��
            return services.BuildDynamicProxyProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // ���𵽲�ͬ��������ʱ����д��127.0.0.1����0.0.0.0����Ϊ�����÷��������ߵ��õĵ�ַ
            string ip = Configuration["ip"];
            int port = int.Parse(Configuration["port"]);
            // ��consulע�����
            ConsulClient client = new ConsulClient(ConfigurationOverview);
            Task<WriteResult> result = client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                // �����ţ������ظ�����Guid���
                ID = "apiservice1" + Guid.NewGuid(),
                // ���������
                Name = "apiservice1",
                // �ҵ�ip��ַ�����Ա�����Ӧ�÷��ʵĵ�ַ�����ز��Կ�����127.0.0.1������������һ��Ҫд�Լ�������ip��ַ��
                Address = ip,
                // �ҵĶ˿�
                Port = port,
                Check = new AgentServiceCheck()
                {
                    // ����ֹͣ��ú�ע��
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // �������ʱ���������߳�Ϊ�������
                    Interval = TimeSpan.FromSeconds(10),
                    // ��������ַ
                    HTTP = $"http://{ip}:{port}/api/health",
                    Timeout = TimeSpan.FromSeconds(5)
                }
            });
        }

        private static void ConfigurationOverview(ConsulClientConfiguration obj)
        {
            obj.Address = new Uri("http://127.0.0.1:8500");
            obj.Datacenter = "dc1";
        }

        /// <summary>
        /// ������������ע��
        /// 
        /// ͨ������ɨ������Service�ֻ࣬Ҫ�����б����CustomInterceptorAttribute�ķ�������������ʵ���ࡣ
        /// Ϊ�˱���һ����ɨ�������࣬����RegisterServices�����ֶ�ָ�����ĸ������м��ء�
        /// </summary>
        private static void RegisterServices(Assembly assembly, IServiceCollection services)
        {
            // ���������е�����public����
            foreach (Type type in assembly.GetExportedTypes())
            {
                // �ж������Ƿ��б�ע��CustomInterceptorAttribute�ķ���
                bool hasHystrixCommandAttr = type.GetMethods().Any(m => m.GetCustomAttribute(typeof(HystrixCommandAttribute)) != null);
                if (hasHystrixCommandAttr)
                {
                    services.AddSingleton(type);
                }
            }
        }
    }
}
