using CodeBase.Logic.Player;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	private Vector3 mousePosition;
	private Camera mainCam;
	private Rigidbody2D rb;
	public float force;

	private void Start()
	{
		mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		rb = GetComponent<Rigidbody2D>();

		mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
		Vector3 direction = mousePosition - transform.position;

		rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;

		float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log($"Bullet hit: {collision.name}");

		// Check if the object hit has the PlayerMovement component
		PlayerMovement player = FindObjectOfType<PlayerMovement>();
		if (player != null)
		{
			player.TeleportTo(transform.position);
		}

		Destroy(gameObject);
	}
}
