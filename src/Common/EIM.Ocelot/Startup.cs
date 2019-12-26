using EIM.AppMetrics;
using EIM.AspNetCore;
using EIM.Exceptionless;
using EIM.Exceptionless.Logging;
using EIM.Exceptionless.Model;
using Exceptionless;
using Exceptionless.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using Ocelot.Administration;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EIM.Ocelot
{
    public class Startup
    {
        /// <summary>
        /// ���캯������ʼ��������Ϣ
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ϵͳ����
        /// </summary>
        public IConfiguration Configuration { get; }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authority = Configuration.GetValue<string>("Authority");
            var collectorUrl = Configuration.GetValue<string>("Skywalking:CollectorUrl");

            Action<IdentityServerAuthenticationOptions> options = o =>
            {
                o.Authority = authority;
                o.ApiName = "api";
                o.SupportedTokens = SupportedTokens.Both;
                o.ApiSecret = "secret";
            };

            var authenticationProviderKey = "apikey";
            Action<IdentityServerAuthenticationOptions> options2 = o =>
            {
                o.Authority = "http://127.0.0.1:5000";
                o.ApiName = "api1";
                o.RequireHttpsMetadata = false;
            };

            services.AddAuthentication()
            .AddIdentityServerAuthentication(authenticationProviderKey, options2);

            services.AddOcelot()
                .AddCacheManager(x =>
                {
                    x.WithDictionaryHandle();
                })
                 .AddConsul()
                 .AddConfigStoredInConsul()
                 .AddPolly()
                 .AddAdministration("/administration", options);

            services.AddNanoFabricConsul(Configuration);
            services.AddNanoFabricExceptionless();

            services.AddAppMetrics(x =>
            {
                var opt = Configuration.GetSection("AppMetrics").Get<AppMetricsOptions>();
                x.App = opt.App;
                x.ConnectionString = opt.ConnectionString;
                x.DataBaseName = opt.DataBaseName;
                x.Env = opt.Env;
                x.Password = opt.Password;
                x.UserName = opt.UserName;
            });

            //services.AddSkyWalking(option =>
            //{
            //    option.DirectServers = collectorUrl;
            //    option.ApplicationCode = "nanofabric_ocelot";
            //});
        }

        // http://edi.wang/post/2017/11/1/use-nlog-aspnet-20
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            app.UseExceptionless(Configuration);
            env.ConfigureNLog($"{env.ContentRootPath}{ Path.DirectorySeparatorChar}nlog.config");
            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();
            loggerFactory.AddExceptionless();

            app.UseConsulRegisterService(Configuration);

            app.UseOcelot().Wait();
            app.UseAppMetrics();
            ExceptionlessClient.Default.SubmittingEvent += Default_SubmittingEvent;

            string tagName = "��Ϣ��ǩ";//�Զ����ǩ
            var data = new ExcDataParam() { Name = "�������", Data = new { Id = 001, Name = "����" } };//�Զ��嵥��model
            var user = new ExcUserParam() { Id = "No0001", Name = "������", Email = "geffzhang@live.cn", Description = "�߼���������ʦ" };//�û���Ϣ
            var datas = new List<ExcDataParam>() {
                new ExcDataParam(){
                    Name ="�������",
                    Data =new { Id = 002, Name = "����"
                    }
                },
                new ExcDataParam(){
                    Name ="���ؽ��",
                    Data =new { Id = 003, Name = "����"
                    }
                }
            };

            ILessLog lessLog = app.ApplicationServices.GetRequiredService<ILessLog>();
            lessLog.Info("���û�&�Զ�������&��ǩ: Application - Configure is invoked", user, datas, tagName);
        }

        /// <summary>
        /// Ĭ���ύ�쳣�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Default_SubmittingEvent(object sender, EventSubmittingEventArgs e)
        {
            var argEvent = e.Event;
            if (argEvent.Type == Event.KnownTypes.Log && argEvent.Source == "Ocelot.Configuration.Repository.FileConfigurationPoller")
            {
                e.Cancel = true;
                return;
            }
            // ֻ����δ������쳣
            if (!e.IsUnhandledError)
                return;

            //����û�д�����Ĵ���
            var error = argEvent.GetError();
            if (error == null)
                return;

            // ����404����
            if (e.Event.IsNotFound())
            {
                e.Cancel = true;
                return;
            }

            //����401(Unauthorized)��������֤�Ĵ���.
            if (error.Code == "401")
            {
                e.Cancel = true;
                return;
            }

            //�����κ�δ�������׳����쳣
            var handledNamespaces = new List<string> { "Exceptionless" };
            var handledNamespaceList = error.StackTrace.Select(s => s.DeclaringNamespace).Distinct();
            if (!handledNamespaceList.Any(ns => handledNamespaces.Any(ns.Contains)))
            {
                e.Cancel = true;
                return;
            }

            e.Event.Tags.Add("δ�����쳣");//���ϵͳ�쳣��ǩ
            e.Event.MarkAsCritical();//���Ϊ�ؼ��쳣
        }
    }
}
