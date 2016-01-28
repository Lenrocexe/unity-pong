using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public float speed = 30;

	// Use this for initialization
	void Start () 
	{
		GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		// Note: 'col' holds the collision information. If the
		// Ball collided with a racket, then:
		//   col.gameObject is the racket
		//   col.transform.position is the racket's position
		//   col.collider is the racket's collider

		float x = col.gameObject.name == "Player 1" ? 1 : -1;

		// Not sure why yet but .position is a Vector3 while our function expects a Vector2.
		// Maybe Vector3 is also a Vector2 and thus can be downcast?
		float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);

		Vector2 dir = new Vector2(x, y).normalized;

		GetComponent<Rigidbody2D>().velocity = dir * speed;
	}

	float hitFactor (Vector2 ballPos, Vector2 racketPos, float racketHeight)
	{
		// ascii art:
		// ||  1 <- at the top of the racket
		// ||
		// ||  0 <- at the middle of the racket
		// ||
		// || -1 <- at the bottom of the racket

		// This does not seem to work correctly yet.
		// The top part still translates to 0 as the ball moves horizontally.
		// Only when it touches the top side of the racket will it move in an angle.
		// This is only possible by moving the sides of the racket into the ball.

		return (ballPos.y - racketPos.y) / racketHeight;
	}
}
