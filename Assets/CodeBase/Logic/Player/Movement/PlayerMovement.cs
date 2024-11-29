using System;
using System.Collections;
using CodeBase.Services.CameraProvider;
using CodeBase.Services.InputService;
using CodeBase.StaticData.ScriptableObjects;
using CodeBase.Tools;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
	public class PlayerMovement : MonoBehaviour, IPlayerDataRequired
	{
		private Rigidbody2D _rigidbody;
		private IInputService _inputService;
		private ICameraProvider _cameraProvider;

		private float _jumpReloadTime;
		private int _jumpForce = 800;
		private bool _canJump => _isGrounded && !_isOnSlope && _isJumpReloaded;

		private bool _isGrounded;
		private bool _isOnSlope;
		private bool _isJumpReloaded = true;

		[SerializeField]
		private BoxCollider2D _playerSpaceCollider;

		[SerializeField]
		private float _slopeAngleLimit = 30f; // Maximum angle for slopes
		[SerializeField]
		private float _wallAngleLimit = 75f;  // Minimum angle for walls

		private float _collisionAdjustmentAmount;
		private IRaycasterService _raycasterService;

		[Inject]
		private void Construct(IInputService inputService, ICameraProvider cameraProvider, IRaycasterService raycasterService)
		{
			_raycasterService = raycasterService;
			_cameraProvider = cameraProvider;
			_inputService = inputService;
		}

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		private void Start()
		{
			_rigidbody.freezeRotation = true;
			_inputService.OnJumpPressed += OnJumpPressed;
		}

		private void OnDestroy()
		{
			_inputService.OnJumpPressed -= OnJumpPressed;
		}

		public void LoadData(PlayerData playerData)
		{
			_rigidbody.gravityScale = playerData.GravityScale;
			_jumpForce = playerData.JumpPower;
			_jumpReloadTime = playerData.PlayerJumpReload;
			_collisionAdjustmentAmount = playerData.CollisionAdjustmentAmount;
		}

		private void OnCollisionStay2D(Collision2D collision)
		{
			foreach (ContactPoint2D contact in collision.contacts)
			{
				Vector2 normal = contact.normal;
				float angle = Vector2.Angle(normal, Vector2.up);

				if (angle <= _slopeAngleLimit)
				{
					// Flat ground or gentle slope
					_isGrounded = true;
					_isOnSlope = false;
					return;
				}
				else if (angle >= _wallAngleLimit)
				{
					// Wall detected (steep surface)
					_isGrounded = true;
					_isOnSlope = false;
					return;
				}
				else
				{
					// Slope detected
					_isOnSlope = true;
				}
			}

			// If no valid ground or wall detected
			_isGrounded = false;
		}

		private void OnCollisionExit2D(Collision2D collision)
		{
			_isGrounded = false;
			_isOnSlope = false;
		}

		public void TeleportTo(Vector3 targetPosition)
		{
			if (!_canJump)
				return;

			transform.position = _raycasterService.AdjustPosition(targetPosition, _playerSpaceCollider.size, _collisionAdjustmentAmount);
		}

		private void OnJumpPressed()
		{
			if (!_canJump)
				return;

			Vector3 posInScreen = _cameraProvider.GetCamera().WorldToScreenPoint(transform.position);

			Vector3 directionToMouse = _inputService.GetScreenMousePosition().ToVector3() - posInScreen;
			directionToMouse.Normalize();

			_rigidbody.AddForce(directionToMouse * _jumpForce);
			StartCoroutine(JumpReload());
		}

		private IEnumerator JumpReload()
		{
			_isJumpReloaded = false;
			yield return new WaitForSeconds(_jumpReloadTime);
			_isJumpReloaded = true;
		}
	}
}
