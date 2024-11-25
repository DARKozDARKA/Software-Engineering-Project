using System;
using CodeBase.Services.InputService;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private bool canJump = false;
		private Rigidbody2D _rigidbody;

		void Start()
        {
			// Cache the Rigidbody2D for better performance
			_rigidbody = GetComponent<Rigidbody2D>();

			// Adjust gravity scale for faster falling
			_rigidbody.gravityScale = 1; // Increase the gravity effect

			// Prevent unwanted rotation
			_rigidbody.freezeRotation = true;
		}
        void Update()
        {   
            if (Input.GetMouseButtonUp(0) && canJump)
            {   // reinitialize forceAmount value at start
                // mouse position
                Vector3 posInScreen = Camera.main.WorldToScreenPoint(transform.position);
                // so compute direction and normalize it
                Vector3 dirToMouse = Input.mousePosition - posInScreen;
                dirToMouse.Normalize();
                // adding the force to the 2D Rigidbody according forceAmount value
                GetComponent<Rigidbody2D>().AddForce(dirToMouse * 200 * 4);
            }
        }

        void OnCollisionStay2D(Collision2D obj)
        {
            canJump = true;
        }

        void OnCollisionExit2D(Collision2D obj)
        {
            canJump = false;
        }
	}
}