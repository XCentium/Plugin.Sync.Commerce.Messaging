using Microsoft.Extensions.Logging;
using Plugin.Sync.Commerce.Messaging.Models;
using Plugin.Sync.Commerce.Messaging.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sync.Commerce.Messaging.Pipelines
{
    public class SendSmsMessagePipeline : CommercePipeline<SendTwilioSmsArgument, SendMessageResult>,
        ISendSmsMessagePipeline, IPipeline<SendTwilioSmsArgument, SendMessageResult, CommercePipelineExecutionContext>, 
        IPipelineBlock<SendTwilioSmsArgument, SendMessageResult, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
        public SendSmsMessagePipeline(IPipelineConfiguration<ISendSmsMessagePipeline> configuration, ILoggerFactory loggerFactory) 
            : base((IPipelineConfiguration)configuration, loggerFactory)
        {
        }
    }
}

  