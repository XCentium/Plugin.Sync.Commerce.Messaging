using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XCentium.Sitecore.Commerce.Messages.Shared
{
    public class SitecoreApiHelper
    {
        public bool SendMessage(CommercePipelineExecutionContext context)
        {
            //var shopName = context.CommerceContext.CurrentShopName();
            //var itemPath = context.GetPolicy<SitecoreControlPanelItemsPolicy>().StorefrontsPath + Constants.Shipping.ForwardSlash + shopName + Constants.Shipping.ForwardSlash + Constants.Shipping.Price;
            //try
            //{
            //    SitecoreConnectionPolicy policy = context.GetPolicy<SitecoreConnectionPolicy>();
            //    var sendMailUrl = "/api/vista/ecommerce/order/sendemail";
            //    HttpResponseMessage responseMessage = SitecoreConnectionManager.ProcessRequest(context, sendMailUrl, "POST", (ItemModel)null);
            //    return (responseMessage.StatusCode == System.Net.HttpStatusCode.OK);
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

            return true;
        }

    }
}
