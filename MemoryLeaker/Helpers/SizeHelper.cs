using System.Web;

namespace MemoryLeaker.Helpers
{
    public class SizeHelper
    {
        public static IHtmlString FormatMB(int mb)
        {
            return new HtmlString(mb.ToString("N0") + "MB");
        }
    }
}