using RazorEngine.Templating;
using RazorEngine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEngineExample
{
    public class MyHtmlHelper
    {
        public IEncodedString Raw(string rawString)
        {
            return new RawString(rawString);
        }
    }

    public abstract class HtmlSupportTemplateBase<T> : TemplateBase<T>
    {
        public HtmlSupportTemplateBase()
        {
            Html = new MyHtmlHelper();
        }

        public MyHtmlHelper Html { get; set; }
    }
}
