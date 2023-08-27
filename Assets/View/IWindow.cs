namespace View
{
    public interface IWindow
    {
        bool IsOpened { get; }
        void Open();
        void Close();
    }
}