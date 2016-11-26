using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEngineExample
{
    public class MyModel
    {
        public string ModelProperty { get; set; }

        public SubModel SubModel { get; set; }

        public string SubModelTemplateName { get; set; }
    }
}
