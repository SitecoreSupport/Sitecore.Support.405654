using System;

namespace Sitecore.Support.Form.Core.Pipelines.ProcessMessage
{
  using Sitecore.Form.Core.Configuration;
  using Sitecore.Form.Core.Controls.Data;
  using Sitecore.Form.Core.Pipelines.ProcessMessage;
  using Forms.Core.Data;
  using StringExtensions;

  public class ExpandTokens
  {
    #region New code
    public void Process(ProcessMessageArgs args)
    {
      foreach (AdaptedControlResult result in args.Fields)
      {
        var item = new FieldItem(StaticSettings.ContextDatabase.GetItem(result.FieldID));
        var str = result.Value;

        if (args.MessageType == MessageType.SMS) continue;
        var str2 = args.Mail.ToString();
        var index = str2.IndexOf("[<label id=\"{0}\">".FormatWith(item.ID), StringComparison.Ordinal);

        if (index <= -1) continue;
        var num2 = str2.IndexOf("</label>]", index, StringComparison.Ordinal);

        if (num2 <= -1) continue;
        num2 += "</label>]".Length;
        var oldValue = str2.Substring(index, num2 - index);

        if (!string.IsNullOrEmpty(result.Parameters) && result.Parameters.StartsWith("multipleline") && args.IsBodyHtml)
        {
          str = str.Replace(Environment.NewLine, "<br/>");
        }

        args.Mail.Replace(oldValue, str ?? string.Empty);
      }
    }
    #endregion
  }
}
