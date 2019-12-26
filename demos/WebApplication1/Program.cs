using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WebApplication1.Helper;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var config = new ConfigurationBuilder().AddCommandLine(args).Build();
                    string ip = config["ip"];
                    string port = config["port"];

                    // �ڳ���������ʱ�����port=0����û��ָ��port�����Լ�����GetRandAvailablePort��ȡ���ö˿ڡ�
                    if (port == "0")
                    {
                        port = Tools.GetRandAvailablePort().ToString();
                    }

                    webBuilder.UseStartup<Startup>().UseUrls($"http://{ip}:{port}");
                });
    }
}
