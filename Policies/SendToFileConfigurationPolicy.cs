using Sitecore.Commerce.Core;

namespace XCentium.Sitecore.Commerce.Messages.Policies
{
    /// <summary>
    /// SMTP Server configuration
    /// </summary>
    public class SendToFileConfigurationPolicy : Policy
    {
        public SendToFileConfigurationPolicy()
        {
        }

        public string FolderPath { get; set; }
        public string FileNamePrefix { get; set; }
        public string FileNameSuffix { get; set; }
        public string FileExtension { get; set; }
    }
}
