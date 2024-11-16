namespace CodeBase.Services.InputService
{
    public interface IInputServiceProvider
    {
        void SetUpButtonDown();
        void SetUpButtonUp();
        bool GetUpButton();
    }
}