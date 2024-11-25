using CodeBase.Services.CameraProvider;
using CodeBase.Services.InputService;
using CodeBase.Tools;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class PlayerMovement : MonoBehaviour
    {
		private Rigidbody2D _rigidbody;
        private IInputService _inputService;
        private ICameraProvider _cameraProvider;
        
        [SerializeField]
        private int _jumpForce = 800;
        
        private bool _canJump = false;
        
        [Inject]
        private void Construct(IInputService inputService, ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            _inputService = inputService;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _rigidbody.gravityScale = 1;
            _rigidbody.freezeRotation = true;
            _inputService.OnJumpPressed += OnJumpPressed;
        }

        private void OnDestroy() => 
            _inputService.OnJumpPressed -= OnJumpPressed;

        private void OnCollisionStay2D(Collision2D obj) => 
            _canJump = true;

        private void OnCollisionExit2D(Collision2D obj) => 
            _canJump = false;

        private void OnJumpPressed()
        {
            if (!_canJump)
                return;
            
            Vector3 posInScreen = _cameraProvider.GetCamera().WorldToScreenPoint(transform.position);
            
            Vector3 directionToMouse = _inputService.GetScreenMousePosition().ToVector3() - posInScreen;
            directionToMouse.Normalize();

            _rigidbody.AddForce(directionToMouse * _jumpForce);
        }
	}
}