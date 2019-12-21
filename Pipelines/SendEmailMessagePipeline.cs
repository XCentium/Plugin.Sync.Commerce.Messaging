using Microsoft.Extensions.Logging;
using Plugin.Sync.Commerce.Messaging.Models;
using Plugin.Sync.Commerce.Messaging.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sync.Commerce.Messaging.Pipelines
{
    public class SendEmailMessagePipeline : CommercePipeline<SendEmailArgument, SendMessageResult>,
        ISendEmailMessagePipeline, IPipeline<SendEmailArgument, SendMessageResult, CommercePipelineExecutionContext>, 
        IPipelineBlock<SendEmailArgument, SendMessageResult, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
        public SendEmailMessagePipeline(IPipelineConfiguration<ISendEmailMessagePipeline> configuration, ILoggerFactory loggerFactory) 
            : base((IPipelineConfiguration)configuration, loggerFactory)
        {
        }
    }
}

  