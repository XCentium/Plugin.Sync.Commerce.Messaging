using Sitecore.Commerce.Core;

namespace Plugin.Sync.Commerce.Messaging.Policies
{
    /// <summary>
    /// Configuration policy for Plugin.Sync.Commerce.Messaging.Pipelines.Blocks.SendToFileBlock
    /// </summary>
    public class SendToFileConfigurationPolicy : Policy
    {
        public SendToFileConfigurationPolicy()
        {
        }

        /// <summary>
        /// Path to folder where foles will be saved. Make sure CE has write access to configured path
        /// </summary>
        public string FolderPath { get; set; }

        /// <summary>
        /// Prefix to be added to the beginning of filename on all files created by SendToFileBlock
        /// </summary>
        public string FileNamePrefix { get; set; }

        /// <summary>
        /// Suffix to be added to the end of filename on all files created by SendToFileBlock
        /// </summary>
        public string FileNameSuffix { get; set; }

        /// <summary>
        /// File extension to be added to the end of filename on all files created by SendToFileBlock
        /// </summary>
        public string FileExtension { get; set; }
    }
}
