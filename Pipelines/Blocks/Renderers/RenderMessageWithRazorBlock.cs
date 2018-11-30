using RazorLight;
using Serilog;
using Sitecore.Commerce.Core;
using System;
using System.Threading.Tasks;

namespace XCentium.Sitecore.Commerce.Messages.Pipelines.Blocks.Renderers
{
    /// <summary>
    /// Renders Razor template using entity from CommercePipelineExecutionContext context as a Model
    /// </summary>
    public class RenderMessageWithRazorBlock : PipelineMessageBlockBase
    {
        RazorLightEngine _razorEngine;

#pragma warning disable 1998
        public override async Task<CommerceEntity> Run(CommerceEntity entity, CommercePipelineExecutionContext context)
        {
            try
            {
                if (_razorEngine == null)
                {
                    _razorEngine = new RazorLightEngineBuilder()
                     .UseMemoryCachingProvider()
                     .Build();
                }

                var template = GetTemplate(context);
                var message = new PropertiesModel();

                foreach (var property in template.Properties)
                {
                    if (!string.IsNullOrEmpty(property.Key) && (property.Value as string) != null)
                    {
                        var messageContent = _razorEngine.CompileRenderAsync($"{this.Name}{this.MessageName}{property.Key}", property.Value as string, entity, entity.GetType()).Result;
                        message.SetPropertyValue(property.Key, messageContent);
                    }
                }

                SetMessage(context, message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error in RenderMessageWithRazorBlock. Message ID: {this.MessageName}, Entity ID: {entity?.Id}");
            }

            return entity;
        }
#pragma warning restore 1998
    }
}
