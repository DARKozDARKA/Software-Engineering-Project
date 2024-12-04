using System;
using System.Collections;
using System.ComponentModel;
using CodeBase.Services.CameraProvider;
using CodeBase.Services.InputService;
using CodeBase.StaticData.ScriptableObjects;
using CodeBase.Tools;
using Unity.Collections;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
	public class PlayerMovement : MonoBehaviour, IPlayerDataRequired
	{
		public Action<PlayerMovementState> OnStateChanged;

		private PlayerMovementState _currentState;

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
		private float _slopeAngleLimit = 30f;
		[SerializeField]
		private float _wallAngleLimit = 75f; 

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
		
		public void TeleportTo(Vector3 targetPosition) => 
			transform.position = _raycasterService.AdjustPosition(targetPosition, _playerSpaceCollider.size, _collisionAdjustmentAmount);

		private void OnCollisionStay2D(Collision2D collision) => 
			EvaluateState(collision);

		private void OnCollisionExit2D(Collision2D collision) => 
			EvaluateState(collision);


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
		
		private void EvaluateState(Collision2D collision)
		{
			foreach (ContactPoint2D contact in collision.contacts)
				if (EvaluateContact(contact))
					return;

			_isGrounded = false;

			bool EvaluateContact(ContactPoint2D contact)
			{
				float angle = Vector2.Angle(contact.normal, Vector2.up);

				if (EvaluateGround(angle))
					return true;

				if (EvaluateWall(angle)) 
					return true;

				EvaluateSlope();
				return false;
			}

			bool EvaluateGround(float angle)
			{
				if (!(angle <= _slopeAngleLimit))
					return false;
				
				ChangeState(PlayerMovementState.Default);
				_isGrounded = true;
				_isOnSlope = false;
				return true;

			}

			bool EvaluateWall(float angle)
			{
				if (!(angle >= _wallAngleLimit))
					return false;
				
				ChangeState(PlayerMovementState.Default);
				_isGrounded = true;
				_isOnSlope = false;
				return true;

			}

			void EvaluateSlope()
			{
				ChangeState(PlayerMovementState.Slope);
				_isOnSlope = true;
			}
		}


		private void ChangeState(PlayerMovementState newState)
		{
			if (_currentState == newState)
				return;

			_currentState = newState;
			OnStateChanged?.Invoke(_currentState);
		}
	}
}
