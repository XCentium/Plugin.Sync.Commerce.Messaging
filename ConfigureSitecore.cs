using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;
using Pipelines;
using XCentium.Sitecore.Commerce.Messages.Pipelines;
using XCentium.Sitecore.Commerce.Messages.Pipelines.Blocks;
using System;
using Sitecore.Commerce.Plugin.Orders;
using XCentium.Sitecore.Commerce.Messages.Shared;
using XCentium.Sitecore.Commerce.Messages.Pipelines.Blocks.Renderers;
using XCentium.Sitecore.Commerce.Messages.Pipelines.Blocks.Templates;

namespace XCentium.Sitecore.Commerce.Messages
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
            //services.Sitecore().Pipelines(config => config
            // .AddPipeline<IExportOrdersMinionPipeline, ProcessOrderStatusPipeline>(
            //        configure =>
            //            {
            //                configure.Add<OrderEmailNotificationBlock>("TestName");
            //            })
            //);

            services.Sitecore().Pipelines(config => config
                .ConfigurePipeline<IGetOrderPipeline>(builder => builder
                    .Add<GetTemplateFromSitecoreBlock>((e) =>
                    {
                        e.MessageName = "TestMessage";
                        e.TemplateId = "{1B324C4C-976B-444E-AB67-D41ED60663A0}";
                    })
                    .Add<GetEmailRecipientsBlock>((e) =>
                    {
                        e.MessageName = "TestMessage";
                    })
                    .Add<GetSmsRecipientsBlock>((e) =>
                    {
                        e.MessageName = "TestMessage";
                    })
                    .Add<RenderMessageWithRazorBlock>((e) =>
                    {
                        e.MessageName = "TestMessage";
                    })
                    .Add<SendEmailBlock>((e) =>
                    {
                        e.MessageName = "TestMessage";
                    })
                    .Add<SendSmsBlock>((e) =>
                    {
                        e.MessageName = "TestMessage";
                    })
                    .Add<SendToFileBlock>((e) =>
                    {
                        e.MessageName = "TestMessage";
                    })
                    ));

            services.RegisterAllCommands(assembly);
        }
    }
}