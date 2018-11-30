using Serilog;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Management;
using Sitecore.Framework.Pipelines;
using System;
using System.Linq;
using System.Threading.Tasks;
using XCentium.Sitecore.Commerce.Messages.Models;
using XCentium.Sitecore.Commerce.Messages.Shared;

namespace XCentium.Sitecore.Commerce.Messages.Pipelines.Blocks.Templates
{
    /// <summary>
    /// Gets "Message template" content item from Sitcore and saves it in current pipeline as  PropertiesModel name-value collection
    /// </summary>
    public class GetTemplateFromSitecoreBlock : PipelineMessageBlockBase
    {
        public string TemplateId { get; set; }
        IGetItemByIdPipeline _getItemByIdPipeline;
        public GetTemplateFromSitecoreBlock(IGetItemByIdPipeline getItemByIdPipeline) : base((string) null)
        {
            _getItemByIdPipeline = getItemByIdPipeline;
        }

#pragma warning disable 1998
        public override async Task<CommerceEntity> Run(CommerceEntity entity, CommercePipelineExecutionContext context)
        {
            try
            {
                if (string.IsNullOrEmpty(TemplateId))
                {
                    throw new ArgumentNullException("templateItemId is required");
                }

                var sitecoreTemplateItem = SitecoreUtil.GetSitecoreItem(TemplateId, _getItemByIdPipeline, context);
                if (sitecoreTemplateItem == null)
                {
                    throw new ArgumentException($"Email template not found in Sitecore. Message Name: {this.MessageName}");
                }

                if (sitecoreTemplateItem.Keys.Count == 0 || !sitecoreTemplateItem.Values.Any(v => v != null && !string.IsNullOrEmpty(v as string)))
                {
                    throw new ArgumentException($"Not found any fields with values in email template. Message Name: {this.MessageName}");
                }

                var template = new PropertiesModel();
                
                foreach (var key in sitecoreTemplateItem.Keys)
                {
                    if (key.StartsWith("_") && !string.IsNullOrEmpty(sitecoreTemplateItem[key] as string))
                    {
                        template.SetPropertyValue(key, sitecoreTemplateItem[key]);
                    }
                }

                SetTemplate(context, template);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error in GetEmailTemplateFromSitecoreBlock. Message Name: {this.MessageName}, Entity ID: {entity?.Id}");
            }

            return entity;

        }
#pragma warning restore 1998
    }
}
