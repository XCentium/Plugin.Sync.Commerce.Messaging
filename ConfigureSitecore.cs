using Microsoft.Extensions.DependencyInjection;
using Plugin.Sync.Commerce.Messaging.Pipelines;
using Plugin.Sync.Commerce.Messaging.Pipelines.Blocks;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;
using System.Reflection;
namespace Plugin.Sync.Commerce.Messaging
{


    /// <summary>
    /// The carts configure sitecore class.
    /// </summary>
    public class ConfigureSitecore : IConfigureSitecore
    {
        /// <summary>
        /// The configure services.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(config => config
                .ConfigurePipeline<IConfigureServiceApiPipeline>(configure => configure.Add<ConfigureServiceApiBlock>())
                    .AddPipeline<ISendEmailMessagePipeline, SendEmailMessagePipeline>(
                        configure =>
                        {
                            configure.Add<SendEmailBlock>();
                        })
                    .AddPipeline<ISendSmsMessagePipeline, SendSmsMessagePipeline>(
                        configure =>
                        {
                            configure.Add<SendSmsBlock>();
                        }));

            services.RegisterAllCommands(assembly);

            

            services.RegisterAllCommands(assembly);
        }
    }
}