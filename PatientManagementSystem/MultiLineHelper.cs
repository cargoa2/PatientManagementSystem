using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementSystem
{
    public static class MultiLineHelper
    {
        public static IHtmlString DisplayFormattedFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression)
        {
            string value = Convert.ToString(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);

            if (string.IsNullOrWhiteSpace(value))
            {
                return MvcHtmlString.Empty;
            }

            value = string.Join("<br/>", value.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(HttpUtility.HtmlEncode));

            return new HtmlString(value);
        }
    }
}