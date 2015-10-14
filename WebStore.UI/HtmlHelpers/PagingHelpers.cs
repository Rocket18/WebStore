using System;
using System.Web.Mvc;
using WebStore.UI.Models;
using System.Text;
using System.Web;

namespace WebStore.UI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo paginInfo, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();
            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination pagination-lg");

            // ulTag.InnerHtml += Environment.NewLine;
            // result.Append(ulTag.ToString());
            for (int i = 1; i <= paginInfo.TotalPages; i++)
            {
                var liTag = new TagBuilder("li");
                var aTag = new TagBuilder("a");


                if (i == paginInfo.CurrentPage)
                {
                    liTag.AddCssClass("active");
                    liTag.InnerHtml = "<span>" + i + " <span class=\"sr-only\">(current)</span>";
                }
                else
                {
                    aTag.MergeAttribute("href", pageUrl(i));
                    aTag.InnerHtml = i.ToString();
                }

                liTag.InnerHtml += string.Format("{0}", aTag);
                ulTag.InnerHtml += string.Format("{0} {1}", liTag.ToString(), Environment.NewLine);
            }

            return MvcHtmlString.Create(result.Append(ulTag.ToString()).ToString());
        }
    }
}