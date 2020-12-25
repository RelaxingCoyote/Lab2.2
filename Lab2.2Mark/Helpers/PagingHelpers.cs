using Lab2._2Mark.Models;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Lab2._2Mark.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo, Func<int, string> pageUrl)
        {
            // Переменная для хранения html-разметки
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                // Создаём тег-ссылку на страницу <a></a>
                TagBuilder tag = new TagBuilder("a");
                //<a href = "~/Home/UserIndex?page=i"></a>
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                // если текущая страница, то выделяем ее,
                // добавляя класс
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                // class = "btn btn-default"
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}
