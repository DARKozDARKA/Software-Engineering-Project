using CodeBase.Services.InputService;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _inputService.OnJumpPressed += OnJumpPressed;
        }

        public void OnDestroy()
        {
            _inputService.OnJumpPressed -= OnJumpPressed;
        }

        private void OnJumpPressed()
        {
            // print("hi");
        }

        public void Update()
        {
            // print(_inputService.GetHorizontalDirection());
        }
    }
}