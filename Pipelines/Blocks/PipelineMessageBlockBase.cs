using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;
using System;
using System.Linq;
using XCentium.Sitecore.Commerce.Messages.Models;

namespace XCentium.Sitecore.Commerce.Messages.Pipelines.Blocks
{
    /// <summary>
    /// Base class for "Message blocks"
    /// </summary>
    public abstract class PipelineMessageBlockBase : PipelineBlock<CommerceEntity, CommerceEntity, CommercePipelineExecutionContext>
    {
        public string MessageName { get; set; }

        public PipelineMessageBlockBase() : base((string)null)
        {
        }

        public PipelineMessageBlockBase(string name) : base(name)
        {
        }

        public PipelineMessageBlockBase(string name, string messageId) : base(name)
        {
            MessageName = messageId;    
        }

        protected MessageDataModel GetMessageModel(CommercePipelineExecutionContext context, bool createIfNotFound = false)
        {
            var messageDataModels = context.GetModels<MessageDataModel>();
            
            if (messageDataModels != null && messageDataModels.Count == 1)
            {
                return messageDataModels.First();
            }
            else if (messageDataModels != null && messageDataModels.Count > 1)
            {
                var model = messageDataModels.First(e => e.Name.Equals(this.MessageName));
                if (model != null)
                {
                    return model;
                }
            }

            if (createIfNotFound)
            {
                var model = new MessageDataModel { Name = MessageName };
                context.AddModel(model);
                return model;
            }

            throw new ArgumentException("MessageDataModel not found in current pipeline.");
        }

        public PropertiesModel GetTemplate(CommercePipelineExecutionContext context, bool createIfNotFound = false)
        {
            var model = GetMessageModel(context, createIfNotFound);
            return GetTemplate(model, createIfNotFound);
        }

        public PropertiesModel GetTemplate(MessageDataModel model, bool createIfNotFound)
        {
            if (model.Template != null)
            {
                return model.Template;
            }
            else if (createIfNotFound)
            {
                model.Template = new PropertiesModel();
                return model.Template;
            }

            throw new ArgumentException("Message Template not found in current pipeline.");
        }

        public PropertiesModel GetMessage(CommercePipelineExecutionContext context, bool createIfNotFound = false)
        {
            var model = GetMessageModel(context, createIfNotFound);
            return GetMessage(model, createIfNotFound);
        }

        public PropertiesModel GetMessage(MessageDataModel model, bool createIfNotFound)
        {
            if (model.Message != null)
            {
                return model.Message;
            }
            else if (createIfNotFound)
            {
                model.Message = new PropertiesModel();
                return model.Message;
            }

            throw new ArgumentException("Message Data not found in current pipeline.");
        }


        public PropertiesModel GetRecipient(CommercePipelineExecutionContext context, bool createIfNotFound = false)
        {
            var model = GetMessageModel(context, createIfNotFound);
            return GetRecipient(model, createIfNotFound);
        }

        public PropertiesModel GetRecipient(MessageDataModel model, bool createIfNotFound)
        {
            if (model.Recipient != null)
            {
                return model.Recipient;
            }
            else if (createIfNotFound)
            {
                model.Recipient = new PropertiesModel();
                return model.Recipient;
            }

            throw new ArgumentException("Recipient Data not found in current pipeline.");
        }

        public void SetTemplate(CommercePipelineExecutionContext context, PropertiesModel template)
        {
            var model = GetMessageModel(context, true);
            model.Template = MergeProperties(model.Template, template);
            context.RemoveModel(model);
            context.AddModel(model);
        }

        public void SetMessage(CommercePipelineExecutionContext context, PropertiesModel message)
        {
            var model = GetMessageModel(context, true);
            model.Message = MergeProperties(model.Message, message);
            context.RemoveModel(model);
            context.AddModel(model);
        }

        public void SetRecipient(CommercePipelineExecutionContext context, PropertiesModel recipient)
        {
            var model = GetMessageModel(context, true);
            model.Recipient = MergeProperties(model.Recipient, recipient);
            context.RemoveModel(model);
            context.AddModel(model);
        }

        private PropertiesModel MergeProperties(PropertiesModel target, PropertiesModel source)
        {
            if (target == null)
            {
                target = source;
            }
            foreach (var property in source.Properties)
            {
                target.SetPropertyValue(property.Key, property.Value);
            }

            return target;
        }
    }
}
