namespace SimpleMvc.Framework.ViewEngine
{
    using Interfaces;
    using System;

    public class ActionResult : IActionResult
    {
        public ActionResult(string viewFullQualifedName)
        {
            this.Action = (IRenderable)Activator
                .CreateInstance(Type.GetType(viewFullQualifedName));
        }

        public IRenderable Action { get; private set; }

        public string Invkoe()
            => this.Action.Render();
    }
}
