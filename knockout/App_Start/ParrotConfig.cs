[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(knockout.App_Start.ParrotConfig), "Start")]
namespace knockout.App_Start
{
    using Parrot.AspNet;

    public class ParrotConfig
    {
        public static void Start()
        {
            System.Web.Mvc.ViewEngines.Engines.Add(new ParrotViewEngine());
        }
    }
}