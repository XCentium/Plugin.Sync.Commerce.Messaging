# Sitecore Experience Commerce: Sending messages triggered by Commerce Engine pipeline events

On most Commerce projects there’s a need to send a message to about an event related to cart, order or a customer being updated in the system. For example, an email would be sent to a customer when the order has been submitted successfully. Or system would send a text message to customer's phone number when an order gets shipped if the customer signed up for order tracking message notifications. Or a file in specific format would need to be uploaded to FTP location for processing by external ERP system at some point after an order has been processed by CE minions. There can be many examples where this kind of messages would need to be generated and sent to an external system. We usually address this by creating a custom plugin and injecting it into one the appropriate pipeline in Commerce Engine.

The common pattern for this kind of messages usually looks like this:
1.  Some event is triggered in one of the Commerce pipelines
2.  The system looks up appropriate message template
3.  Using message template and uses that plus Commerce entity from pipeline context to generate message body
4.  The system retrieves information about message recipients from context entity or from configuration (recipients can be actual persons or external systems, such service API, FTP location, etc.)
5.  The system sends a message to recipients

And if there's a common pattern then there's a good chance we can code this in a reusable fashion, so this doesn't have to be implemented again and again on each Commerce project. “XC Commerce Messages” is the library I built to address such scenarios and the rest of this post describes how to use it and extend it if needed. This library is a Visual Studio Commerce Engine project, which can be downloaded from <span>[https://github.com/XCentium/Plugin.Sync.Commerce.Messaging](https://github.com/XCentium/Plugin.Sync.Commerce.Messaging)</span> and included in your Commerce Engine solution. (I'm planning to compile and share it as NuGet package when I get some time to do it right). The library consists of a number of very atomic components that are meant to be combined with other components and form a pipeline to perform a task. That way you can, for example, use provided renderer as is, but add your own code to retrieve a list of recipients. All components in the library are CE plugins, that are meant to be injected into appropriate CE pipelines in ConfigureServices call of ConfigureSitecore class in your base solution. Those plugins can save data in commerce pipeline to be used by plugins that follow (e.g. message template would need to be retrieved before message body can be rendered, email recipients need to be figured out before email can be sent, etc.)

## Plugins

Below plugins are provided by XC Messaging library. We might revise and extend this list and you’re welcome to open source code and modify/extend these as needed.

### Available plugins

#### Retrieve Message Templates

*   **GetTemplateFromSitecoreBlock**. Reads Sitecore content item and retrieves values of fields whose names start with an underscore (e.g. "_Body" or "_Subject")

#### Retrieve Message Recipients

*   **GetEmailRecipientsBlock**. Retrieve "To" email address from context Commerce entity. It will first try to find an email address in Entity’s ContactComponent, if not found, it'll try to read a value from its PhysicalFulfillmentComponent
*   **GetSmsRecipientsBlock**: Retrieve customer phone number from context Commerce entity. Reads customer phone number from PhysicalFulfillmentComponent

#### Render Message

*   **RenderMessageWithRazorBlock**: Render message using given template from context and context Commerce entity using Razor rendering engine.
    *   It will iterate through each child field of “Template” field of MessageDataModel from context, render message content for each, using template value as Razor template and context Commerce Entity as the model. The rendered output is saved in “Message” field of the same MessageDataModel object, which is then saved in a pipeline context to be used by other blocks that follow (e.g. SendEmailBlock)

#### Send Message

*   **SendEmailBlock**: Send email via SMTP, prior to calling this block:
    *   “To” field needs to be populated by GetEmailRecipientsBlock and saved in MessageDataModel.Recipient
    *   “Subject” and “Body” fields need to be rendered by RenderMessageWithRazorBlock and saved in MessageDataModel.Template
*   **SendSmsBlock**: Send a text message with Twilio. You need working Twilio account (register and read more here). SmsTwilioConfigurationPolicy needs to be properly configured. Prior to calling this block:
    *   "Phone" field needs to be populated by GetSmsRecipientsBlock and saved in MessageDataModel.Recipient
    *   "Body" field needs to be rendered by RenderMessageWithRazorBlock and saved in MessageDataModel.Template
*   **SendToFileBlock**:
    *   "Body" field needs to be rendered by RenderMessageWithRazorBlock and saved in MessageDataModel.Template
    *   SendToFileConfigurationPolicy must be properly initialized and CE must be able to write files into the configured folder path

* * *

## Configuration policies

While messages are being generated and sent, modules need pieces of data that can be retrieved dynamically, based on execution context or statically, from the system configuration. Configurable values are stored in CE policies, which need to be configured to fit into the target environment and bootstrapped into target Commerce Environments.

### Available policies

*   **SendToFileConfigurationPolicy**
    *   Description: Configuration policy for SendToFileBlock
    *   Settings:
        *   FolderPath: Path to the folder where files will be saved. Make sure CE has write access to the configured path.
        *   FileNamePrefix: Prefix to be added to the beginning of filename on all files created by SendToFileBlock.
        *   FileNameSuffix: Suffix to be added to the end of the filename on all files created by SendToFileBlock.
        *   FileExtension: File extension to be added to the end of the filename on all files created by SendToFileBlock.
*   **SmsTwilioConfigurationPolicy**
    *   Description: Configuration policy for SendSmsBlock
    *   Settings:
        *   AccountSid: Twilio Account SID (username)
        *   AuthToken: Twilio Authentication token (password)
        *   FromPhone: From phone number to apply to outgoing text messages
*   **SmtpConfigurationPolicy**
    *   Description: SMTP Server configuration for SendEmailBlock
    *   Settings:
        *   FromEmailAddress: From email address to be set on all outgoing emails
        *   FromEmailDisplayName: Display name for "From" email address
        *   Host: SMTP Server host
        *   Port: SMTP server port
        *   EnableSsl:  When set to true then use SSL when sending emails via SMTP server
        *   Password: Password to use for SMTP server connection
        *   UserName: User Name to use for SMTP server connection
