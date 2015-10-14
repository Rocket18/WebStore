using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebStore.UI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Http404(string url)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            var model = new NotFoundViewModel();
            // Если путь относительный ('NotFound' route), тогда его нужно заменить на запрошенный путь
            model.RequestedUrl = Request.Url.OriginalString.Contains(url) & Request.Url.OriginalString != url ?
                Request.Url.OriginalString : url;
            // Предотвращаем зацикливание при равенстве Referrer и Request
            model.ReferrerUrl = Request.UrlReferrer != null &&
                Request.UrlReferrer.OriginalString != model.RequestedUrl ?
                Request.UrlReferrer.OriginalString : null;

            // TODO: добавить реализацию ILogger

            return View("NotFound", model);
        }
        public class NotFoundViewModel
        {
            public string RequestedUrl { get; set; }
            public string ReferrerUrl { get; set; }
        }

    }
}
