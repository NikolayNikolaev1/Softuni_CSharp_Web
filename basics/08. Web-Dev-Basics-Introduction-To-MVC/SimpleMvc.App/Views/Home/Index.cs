namespace SimpleMvc.App.Views.Home
{
    using Framework.Interfaces;

    public class Index : IRenderable
    {
        public string Render()
            => "<h3>Hello MVC!</h3>";
    }
}
