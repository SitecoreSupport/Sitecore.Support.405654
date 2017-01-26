namespace Sitecore.Support.Form.Core.Pipelines.ProcessMessage
{
    using Sitecore.Form.Core.Configuration;
    using Sitecore.Form.Core.Controls.Data;
    using Sitecore.Form.Core.Pipelines.ProcessMessage;
    using Sitecore.Forms.Core.Data;
    using Sitecore.StringExtensions;

    public class ExpandTokens
    {
        #region New code
        public void Process(ProcessMessageArgs args)
        {
            foreach (AdaptedControlResult result in args.Fields)
            {
                FieldItem item = new FieldItem(StaticSettings.ContextDatabase.GetItem(result.FieldID));
                string str = result.Value;
                if (args.MessageType != MessageType.SMS)
                {
                    string str2 = args.Mail.ToString();
                    int index = str2.IndexOf("[<label id=\"{0}\">".FormatWith(new object[] { item.ID }));
                    if (index > -1)
                    {
                        int num2 = str2.IndexOf("</label>]", index);
                        if (num2 > -1)
                        {
                            num2 += "</label>]".Length;
                            string oldValue = str2.Substring(index, num2 - index);
                            args.Mail.Replace(oldValue, str ?? string.Empty);
                        }
                    }
                }
            }
        } 
        #endregion
    }
}
