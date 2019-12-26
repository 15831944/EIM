using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace WebApplication3
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
            services.AddControllers();

            string ip = "localhost";
            int port = 4001;

            // ��consulע�����
            ConsulClient client = new ConsulClient(config => config.Address = new Uri("http://127.0.0.1:8500"));
            Task<WriteResult> result = client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "duanxin" + Guid.NewGuid(), // �����ţ������ظ�����Guid���
                Name = "duanxin", // ���������
                Address = ip, // �ҵ�ip��ַ�����Ա�����Ӧ�÷��ʵĵ�ַ�����ز��Կ�����127.0.0.1������������һ��Ҫд�Լ�������ip��ַ��
                Port = port, // �ҵĶ˿�
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5), // ����ֹͣ��ú�ע��
                    Interval = TimeSpan.FromSeconds(10), // �������ʱ���������߳�Ϊ�������
                    HTTP = $"http://{ip}:{port}/api/health", // ��������ַ
                    Timeout = TimeSpan.FromSeconds(5)
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
