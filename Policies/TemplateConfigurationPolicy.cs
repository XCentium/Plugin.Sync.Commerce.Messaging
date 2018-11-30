using Sitecore.Commerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCentium.Sitecore.Commerce.Messages.Policies
{
    public class TemplateConfigurationPolicy: Policy
    {
        public PropertiesModel Templates { get; set; }
        public string Name { get; set; }
    }
}
