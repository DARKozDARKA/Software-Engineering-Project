using System;
using CodeBase.Services.InputService;
using UnityEngine;
using Zenject;

public class TeleporterProjectile : MonoBehaviour
{
	public Action<TeleporterProjectile> OnSurfaceHit;

	[SerializeField]
	private float _speed = 5;

	private Rigidbody2D _rigidbody;

	private Vector2 _direction;
	
	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		_rigidbody.linearVelocity = new Vector2(_direction.x, _direction.y).normalized * _speed;

		float rotation = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		OnSurfaceHit?.Invoke(this);
		Destroy(gameObject);
	}

	public Vector3 GetPosition() =>
		transform.position;

	public void SetDirection(Vector2 direction) =>
		_direction = direction;
}
