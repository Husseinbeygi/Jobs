namespace Infrastructure.TagHelpers;

[Microsoft.AspNetCore.Razor.TagHelpers.HtmlTargetElement
	(tag: "button-verify",
	ParentTag = "section-form-buttons",
	TagStructure = Microsoft.AspNetCore.Razor.TagHelpers.TagStructure.WithoutEndTag)]
public class ButtonVerifyTagHelper :
	Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
{
	public ButtonVerifyTagHelper() : base()
	{
	}

	public override void Process
		(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context,
		Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
	{
		// **************************************************
		var icon =
			Utility.GetIconVerify();
		// **************************************************

		// **************************************************
		var body =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "button");

		body.Attributes.Add
			(key: "type", value: "submit");

		body.AddCssClass(value: "btn");
		body.AddCssClass(value: "btn-success");

		body.InnerHtml.AppendHtml(content: icon);
		body.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Verify);
		// **************************************************

		// **************************************************
		output.TagName = null;

		output.TagMode =
			Microsoft.AspNetCore.Razor
			.TagHelpers.TagMode.StartTagAndEndTag;

		output.Content.SetHtmlContent(htmlContent: body);
		// **************************************************
	}
}