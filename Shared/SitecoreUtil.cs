using Serilog;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Management;
using Sitecore.Framework.Pipelines;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCentium.Sitecore.Commerce.Messages.Shared
{
    public static class SitecoreUtil
    {
        public static ItemModel GetSitecoreItem(string itemId, IGetItemByIdPipeline getItemByIdPipeline, CommercePipelineExecutionContext context)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentNullException("itemId");
            }
            if (context == null || context.CommerceContext == null)
            {
                throw new ArgumentNullException("context");
            }

            var language = context.CommerceContext.CurrentLanguage();
            if (language == null)
            {
                throw new ApplicationException("Current Language canot be idetified - check language configuration in Commerce Control Panel");
            }

            var itemModelArgument = new ItemModelArgument(itemId)
            {
                Language = language,
            };

            var options = new PipelineExecutionContextOptions();

            var taskResult = Task.Run<ItemModel>(async () => await getItemByIdPipeline.Run(itemModelArgument, context));
            if (taskResult != null || taskResult.Result != null)
            {
                return taskResult.Result;
            }

            throw new KeyNotFoundException();
        }
    }
}
