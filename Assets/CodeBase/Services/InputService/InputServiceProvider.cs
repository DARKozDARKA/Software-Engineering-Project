namespace CodeBase.Services.InputService
{
    public class InputServiceProvider : IInputServiceProvider
    {
        private bool _upButtonIsPressed;

        public void SetUpButtonDown()
        {
            _upButtonIsPressed = true;
        }

        public void SetUpButtonUp()
        {
            _upButtonIsPressed = false;
        }

        public bool GetUpButton() =>
            _upButtonIsPressed;
    }
}