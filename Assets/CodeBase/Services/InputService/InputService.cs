namespace CodeBase.Services.InputService
{
    public class InputService : IInputService
    {
        private IInputServiceProvider _provider;

        public InputService(IInputServiceProvider provider) => 
            _provider = provider;

        public bool GetUpButton() => 
            _provider.GetUpButton();
    }
}