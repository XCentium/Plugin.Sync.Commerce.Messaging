using Plugin.Sync.Commerce.Messaging.Models;
using Plugin.Sync.Commerce.Messaging.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sync.Commerce.Messaging.Pipelines
{
    [PipelineDisplayName("SendEmailMessagePipeline")]
    public interface ISendEmailMessagePipeline : IPipeline<SendEmailArgument, SendMessageResult, CommercePipelineExecutionContext>, 
        IPipelineBlock<SendEmailArgument, SendMessageResult, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
    }
}