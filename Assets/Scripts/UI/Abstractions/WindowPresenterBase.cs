namespace UI
{
    public abstract class WindowPresenterBase
    {
        protected WindowPresenterBase(ICanvasProvider provider, IWindowView view)
        {
            view.SetParent(provider.Canvas);
        }
    }
}