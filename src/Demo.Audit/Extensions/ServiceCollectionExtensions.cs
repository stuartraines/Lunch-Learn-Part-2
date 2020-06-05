using Audit.Core;
using System.Linq;
using System.Reflection;
using Demo.Audit.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using Nest;
using Audit.Elasticsearch.Providers;
using Microsoft.Extensions.Configuration;
using Demo.Audit.Settings;
using Elasticsearch.Net.Aws;
using Elasticsearch.Net;

namespace Demo.Audit.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAuditServices(this IServiceCollection services, IConfiguration configuration, Action<AuditScope> onSavingAction)
        {            
            var auditSettings = new AuditSettings();
            configuration.Bind(nameof(AuditSettings), auditSettings);

            Configuration.AddOnSavingAction(scope => onSavingAction(scope));
            
            Configuration.Setup()
                .UseElasticsearch(config => config
                    .ConnectionSettings(new AuditConnectionSettings(new SingleNodeConnectionPool(new Uri(auditSettings.Url)), new AwsHttpConnection(configuration.GetAWSOptions())))
                    .Index(auditEvent => $"demoapi.{auditEvent.EventType.ToLowerInvariant()}")
                    .Id(ev => Guid.NewGuid()));

            services.AddSingleton<IElasticClient>(provider => new ElasticClient(((ElasticsearchDataProvider)Configuration.DataProvider).ConnectionSettings)); 

            return services;         
        }

        public static void RegisterAllAuditableTypes(this IServiceCollection services, Assembly[] assemblies)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(IAuditable))));
            foreach (var type in typesFromAssemblies)
            {
                services.AddTransient(type);
                
                var interfaces = type.GetInterfaces().Except(new[] { typeof(IAuditable)});
                foreach (var iface in interfaces)
                {
                    services.AddTransient(iface, provider => AuditProxy.Create(iface, provider.GetRequiredService(type)));
                }
            }
        }            
    }
}