namespace SimpleMvc.Framework.Routers
{
    using Attributes.Methods;
    using Controllers;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using WebServer.Contracts;
    using WebServer.Enums;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;

    public class ControllerRouter : IHandleable
    {
        private IDictionary<string, string> getParams;
        private IDictionary<string, string> postParams;
        private string requestMethod;
        private string controllerName;
        private string actionName;
        private object[] methodParams;

        public IHttpResponse Handle(IHttpRequest request)
        {
            this.getParams = request.UrlParameters;
            this.postParams = request.FormData;
            this.requestMethod = request.Method.ToString();

            string[] urlArgs = request
                .Url
                .Split(new[] { '/', '?' },
                    StringSplitOptions.RemoveEmptyEntries);

            if (urlArgs.Length <= 2)
            {
                string controller = urlArgs[1];
                string action = urlArgs[2];

                this.controllerName = string.Concat(
                    char.ToUpper(controller[0]),
                    controller.Substring(1),
                    "Controller");

                this.actionName = string.Concat(
                    char.ToUpper(action[0]),
                    action.Substring(1));
            }

            MethodInfo method = this.GetMethod();

            if (method == null)
            {
                return new NotFoundResponse();
            }

            IEnumerable<ParameterInfo> parameters = method.GetParameters();
            this.methodParams = new object[parameters.Count()];

            int index = 0;

            foreach (ParameterInfo param in parameters)
            {
                if (param.ParameterType.IsPrimitive ||
                    param.ParameterType == typeof(string))
                {
                    object value = this.getParams[param.Name];
                    this.methodParams[index] = Convert.ChangeType(value, param.ParameterType);
                    index++;
                }
                else
                {
                    Type bindingModelType = param.ParameterType;
                    object bindingModel = Activator.CreateInstance(bindingModelType);

                    IEnumerable<PropertyInfo> properties = bindingModelType.GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        property.SetValue(
                            bindingModel,
                            Convert.ChangeType(
                                this.postParams[property.Name],
                                property.PropertyType));
                    }

                    this.methodParams[index] = Convert.ChangeType(bindingModel, bindingModelType);
                    index++;
                }
            }

            IInvocable actionResult = (IInvocable)this.GetMethod()
                .Invoke(this.GetController(), this.methodParams);

            string content = actionResult.Invkoe();
            IHttpResponse response = new ContentResponse(HttpStatusCode.Ok, content);

            return response;
        }

        private MethodInfo GetMethod()
        {
            MethodInfo method = null;

            foreach (MethodInfo methodInfo in this.GetSuitableMethods())
            {
                IEnumerable<Attribute> attributes = methodInfo
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute);

                if (!attributes.Any() && this.requestMethod.Equals("GET"))
                {
                    return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.IsValid(this.requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

            return method;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods()
        {
            Controller controller = this.GetController();

            if (controller == null)
            {
                return new MethodInfo[0];
            }

            return this.GetController()
                .GetType()
                .GetMethods()
                .Where(m => m.Name == this.actionName);
        }

        private Controller GetController()
        {
            string controllerFullQualifiedName = string.Format(
                "{0}.{1}.{2}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllersFolder,
                this.controllerName);

            Type type = Type.GetType(controllerFullQualifiedName);

            if (type == null)
            {
                return null;
            }

            Controller controller = (Controller)Activator.CreateInstance(type);

            return controller;
        }
    }
}
