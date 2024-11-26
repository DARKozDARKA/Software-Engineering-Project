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
			_inputService.OnFirePressed += OnTeleportPressed;
		}

        private void OnDestroy()
        {
			_inputService.OnJumpPressed -= OnJumpPressed;
            _inputService.OnFirePressed -= OnTeleportPressed;
		}

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

		private void OnTeleportPressed()
		{
			if (!_canJump)
				return;

			// Get the player's current position and mouse position in world coordinates
			Vector3 currentPosition = transform.position;
			Vector3 targetScreenPosition = _inputService.GetScreenMousePosition().ToVector3();
			Vector3 targetWorldPosition = _cameraProvider.GetCamera().ScreenToWorldPoint(targetScreenPosition);

			// Ensure the z-coordinate matches the player's z-position
			targetWorldPosition.z = currentPosition.z;

			// Calculate the direction and capped teleport position
			Vector3 direction = (targetWorldPosition - currentPosition).normalized;
			Vector3 teleportPosition = currentPosition + direction * Mathf.Min(10f, Vector3.Distance(currentPosition, targetWorldPosition));

			// Check for collisions at the teleport position
			float radius = 0.5f; // Define a small radius for the check
			LayerMask collisionMask = LayerMask.GetMask("Default"); // Adjust this to the layers you want to check
			Collider2D overlap = Physics2D.OverlapCircle(teleportPosition, radius, collisionMask);

			if (overlap == null)
			{
				// Safe to teleport
				transform.position = teleportPosition;
			}
			else
			{
				// Cancel teleportation or handle differently (e.g., show feedback)
				Debug.Log("Teleportation blocked by: " + overlap.name);
			}
		}

	}
}