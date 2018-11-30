using Serilog;
using Sitecore.Commerce.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace XCentium.Sitecore.Commerce.Messages.Pipelines.Blocks
{
    /// <summary>
    /// Sends SMS message with Twilio, more details here: https://www.twilio.com/
    /// </summary>
    public class SendToFileBlock : PipelineMessageBlockBase
    {
#pragma warning disable 1998
        public override async Task<CommerceEntity> Run(CommerceEntity entity, CommercePipelineExecutionContext context)
        {
            try
            {
                var message = GetMessage(context);

                if (message.Properties.Count == 0)
                {
                    throw new ArgumentException($"No data to write to file");
                }

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(Environment.CurrentDirectory, $"{Guid.NewGuid()}.txt")))
                {
                    foreach (var property in message.Properties)
                    {
                        outputFile.WriteLine(property.Value);
                    }
                }

                return entity;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error in SendToFileBlock. Message Name: {this.MessageName}, Entity ID: {entity?.Id}");
                return entity;
            }

        }
#pragma warning restore 1998
    }
}
