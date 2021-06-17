namespace SimpleMvc.Framework.Controllers
{
    using Interfaces;
    using Interfaces.Generic;
    using System.Runtime.CompilerServices;
    using ViewEngine;
    using ViewEngine.Generic;

    public class Controller
    {
        private const string NameTemplate = "{0}.{1}.{2}.{3}, {0}";

        protected IActionResult View([CallerMemberName] string caller = "")
        {
            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Get.ControllersSuffix, string.Empty);

            string fullQualifiedName = string.Format(
                NameTemplate,
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ViewsFolder,
                controllerName,
                caller);

            return new ActionResult(fullQualifiedName);
        }

        protected IActionResult<T> View<T>(T model, [CallerMemberName] string caller = "")
        {
            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Get.ControllersSuffix, string.Empty);

            string fullQualifiedName = string.Format(
                NameTemplate,
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ViewsFolder,
                controllerName,
                caller);

            return new ActionResult<T>(fullQualifiedName, model);
        }

        protected IActionResult View(string controller, string action)
        {
            string fullQualifiedName = string.Format(
                NameTemplate,
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ViewsFolder,
                controller,
                action);

            return new ActionResult(fullQualifiedName);
        }

        protected IActionResult<T> View<T>(T model, string controller, string action)
        {
            string fullQualifiedName = string.Format(
                NameTemplate,
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ViewsFolder,
                controller,
                action);

            return new ActionResult<T>(fullQualifiedName, model);
        }
    }
}
