using Plugin.Sync.Commerce.Messaging.Models;
using Plugin.Sync.Commerce.Messaging.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sync.Commerce.Messaging.Pipelines
{
    [PipelineDisplayName("SendSmsMessagePipeline")]
    public interface ISendSmsMessagePipeline : IPipeline<SendTwilioSmsArgument, SendMessageResult, CommercePipelineExecutionContext>, 
        IPipelineBlock<SendTwilioSmsArgument, SendMessageResult, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
    }
}