using Sitecore.Commerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCentium.Sitecore.Commerce.Messages.Models
{
    /// <summary>
    /// Data model to share between blocks while generating and sending messages
    /// </summary>
    public class MessageDataModel : Model
    {
        /// <summary>
        /// Message templates
        /// </summary>
        public PropertiesModel Template { get; set; } = new PropertiesModel();

        /// <summary>
        /// Message content to be sent
        /// </summary>
        public PropertiesModel Message { get; set; } = new PropertiesModel();

        /// <summary>
        /// Recipients of the message(s). Can be an email address(es), phone number(s), file path(s), service URL(s), etc.
        /// </summary>
        public PropertiesModel Recipient { get; set; } = new PropertiesModel();
    }
}
