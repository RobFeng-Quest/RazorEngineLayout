using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RazorEngineExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        void LoadHtml()
        {


            var config = new TemplateServiceConfiguration();
            // You can use the @inherits directive instead (this is the fallback if no @inherits is found).
            config.BaseTemplateType = typeof(TemplateBase<>);
            // Custom template manager http://antaris.github.io/RazorEngine/TemplateManager.html
            //config.TemplateManager = new MyTemplateManager();
            //config.EncodedStringFactory = new RawStringFactory(); // Raw string encoding.
            config.BaseTemplateType = typeof(HtmlSupportTemplateBase<>);

            using (var service = RazorEngineService.Create(config))
            {
                GenerateHtml(service);
            }
        }

        private void GenerateHtml(IRazorEngineService service)
        {
            var SampleTemplate = File.ReadAllText("SampleTemplate.cshtml");
            var SubSample = File.ReadAllText("SubSample.cshtml");

            //service.AddTemplate("part", SubSample);

            // If you leave the second and third parameters out the current model will be used.
            // If you leave the third we assume the template can be used for multiple types and use "dynamic".
            // If the second parameter is null (which is default) the third parameter is ignored.
            // To workaround in the case you want to specify type "dynamic" without specifying a model use Include("p", new object(), null)
            //service.AddTemplate("template", SampleTemplate);

            service.Compile(SampleTemplate, "template", typeof(MyModel));
            service.Compile(SubSample, "part", typeof(SubModel));

            var result = service.Run("template", typeof(MyModel), new MyModel
            {
                SubModelTemplateName = "part",
                ModelProperty = "model1<@><b>rob</b></@>.com &lt;",
                RawProperty = "model1<@><b>rob</b></@>.com &lt;",
                SubModel = new SubModel { SubModelProperty = "submodel2" }
            });

            Console.WriteLine("Result is: {0}", result);

            DisplayHtml(result);
        }


        private void DisplayHtml(string aHtml)
        {
            try
            {
                txtHtml.Text = aHtml;
                wwwHtml.NavigateToString(aHtml);
            }
            catch
            {
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadHtml();
        }
    }
}
