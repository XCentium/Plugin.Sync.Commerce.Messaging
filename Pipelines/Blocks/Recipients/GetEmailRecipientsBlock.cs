using Serilog;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Fulfillment;
using Sitecore.Commerce.Plugin.Management;
using Sitecore.Framework.Pipelines;
using System;
using System.Linq;
using System.Threading.Tasks;
using XCentium.Sitecore.Commerce.Messages.Models;

namespace XCentium.Sitecore.Commerce.Messages.Pipelines.Blocks
{
    /// <summary>
    /// Retrieve "To" email address from context Commerce entity (Order, Cart, etc.)
    /// </summary>
    public class GetEmailRecipientsBlock : PipelineMessageBlockBase
    {
#pragma warning disable 1998
        public override async Task<CommerceEntity> Run(CommerceEntity entity, CommercePipelineExecutionContext context)
        {
            try
            {
                var recipient = new PropertiesModel();
                if (entity.HasComponent<ContactComponent>())
                {
                    var contactComponent = entity.GetComponent<ContactComponent>();
                    if (contactComponent != null && !string.IsNullOrEmpty(contactComponent.Email))
                    {
                        recipient.SetPropertyValue("To", contactComponent.Email);
                    }
                }

                if (entity.HasComponent<PhysicalFulfillmentComponent>())
                {
                    var physicalFulfillmentComponent = entity.GetComponent<PhysicalFulfillmentComponent>();
                    if (physicalFulfillmentComponent != null)
                    {
                        if (physicalFulfillmentComponent.ShippingParty != null && !string.IsNullOrEmpty(physicalFulfillmentComponent.ShippingParty.Email))
                        {
                            recipient.SetPropertyValue(Constants.Keys.To, physicalFulfillmentComponent.ShippingParty.Email);
                        }
                    }
                }
                else
                {
                    throw new ArgumentNullException("Email Address not found.");
                }

                SetRecipient(context, recipient);
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
