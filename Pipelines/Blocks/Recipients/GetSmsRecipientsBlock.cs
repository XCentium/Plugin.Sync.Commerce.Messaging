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
    /// Retrieve customer phone number from context Commerce entity (Order, Cart, etc.)
    /// </summary>
    public class GetSmsRecipientsBlock : PipelineMessageBlockBase
    {
#pragma warning disable 1998
        public override async Task<CommerceEntity> Run(CommerceEntity entity, CommercePipelineExecutionContext context)
        {
            try
            {
                var recipient = new PropertiesModel();

                if (entity.HasComponent<PhysicalFulfillmentComponent>())
                {
                    var physicalFulfillmentComponent = entity.GetComponent<PhysicalFulfillmentComponent>();
                    if (physicalFulfillmentComponent != null)
                    {
                        if (physicalFulfillmentComponent.ShippingParty != null && !string.IsNullOrEmpty(physicalFulfillmentComponent.ShippingParty.Email))
                        {
                            recipient.SetPropertyValue(Constants.Keys.PhoneNumber, physicalFulfillmentComponent.ShippingParty.PhoneNumber);
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
                Log.Error(ex, $"Error in GetSmsRecipientsBlock. Message Name: {this.MessageName}, Entity ID: {entity?.Id}");
            }

            return entity;
        }
#pragma warning restore 1998
    }
}
