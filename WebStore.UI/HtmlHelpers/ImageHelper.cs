using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebStore.UI.HtmlHelpers
{
    public static class ImageHelper
    {
        public static MvcHtmlString Image(this HtmlHelper html, string src, string altText, object htmlAttributes=null)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            builder.MergeAttributes(attributes);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}