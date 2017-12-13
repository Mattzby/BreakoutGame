using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	// Movement Speed
	public float speed;
	public float maxSpeedModifier;
	public float minSpeedModifier;

	//Pass in color list from variable set in level settings
	public Color[] colors;

	private Rigidbody2D ballRigidBody;
	private SpriteRenderer ballSprite;
	private int currentColorIndex;
	private float speedModifier;
	private bool playerHitBall;
	//private Color currentColor;

	// Use this for initialization
	void Start () {
		ballSprite = GetComponent<SpriteRenderer> ();
		ballRigidBody = GetComponent<Rigidbody2D> ();

		speedModifier = 1;
		currentColorIndex = 0;
		playerHitBall = true;

		//set initial ball color
		ballSprite.color = colors [currentColorIndex];

		GameController.getInstance ().SetBallSpeedText (speedModifier);
	}

	//TODO: put velocity changes here... doesn't make sense, i dont want the speed increasing every frame...
	/*void FixedUpdate(){
		ballRigidBody.velocity = ballRigidBody.velocity * speed * speedModifier;
	}*/

	float hitFactor(Vector2 ballPos, Vector2 racketPos,
		float racketWidth) {
		// ascii art:
		//
		// 1  -0.5  0  0.5   1  <- x value
		// ===================  <- racket
		//
		return (ballPos.x - racketPos.x) / racketWidth;
	}

	void OnCollisionEnter2D(Collision2D col) {
		// Hit the Racket?
		if (col.gameObject.CompareTag ("Player")) {
			//Debug.Log ("BALL HIT THE PLAYER!");
			playerHitBall = true;			
			// Calculate hit Factor
			float x = hitFactor (transform.position,
				         col.transform.position,
				         col.collider.bounds.size.x);

			// Calculate direction, set length to 1
			Vector2 dir = new Vector2 (x, 1).normalized;
			ballRigidBody.velocity = dir * speed * speedModifier;

			// Set Velocity with dir * speed
			/*
			if (currentSpeed == startingSpeed) {
				ballRigidBody.velocity = dir * startingSpeed;
			} else {
				ballRigidBody.velocity = dir * currentSpeed;
			}
			*/
							
		} else if (col.gameObject.CompareTag("Border")) {
			//Debug.Log ("BALL HIT THE BORDER!");
			//playerHitBall = false;			
		}
	}

	public void ChangeColor () {

		//Random Color
		//int randomIndex = Random.Range(0,colorList.Count);
		//ballSprite.color = colorList[randomIndex];

		//Cycle Color
		currentColorIndex = currentColorIndex + 1;
		if (currentColorIndex == colors.Length) {
			currentColorIndex = 0;
		}
		ballSprite.color = colors [currentColorIndex];
	}

	public void AdjustBallSpeedModifier (float ballSpeedAdjustment) {

		//Debug.Log ("OLD BALL SPEED MODIFIER = " + speedModifier);

		float newBallSpeedModifier = speedModifier + ballSpeedAdjustment;

		if (newBallSpeedModifier > maxSpeedModifier) {
			speedModifier = maxSpeedModifier;
		} else if (newBallSpeedModifier < minSpeedModifier) {
			speedModifier = minSpeedModifier;
		} else {
			speedModifier = newBallSpeedModifier;
		}

		GameController.getInstance ().SetBallSpeedText (speedModifier);
		//Debug.Log ("NEW BALL SPEED MODIFIER = " + speedModifier);
	}

	public float GetBallSpeedModifier() {
		return speedModifier;		
	}

	/*
	public void SetBallSpeed (float newBallSpeed) {

		Debug.Log ("OLD BALL SPEED = " + currentSpeed);

		if (newBallSpeed > maxSpeed) {
			currentSpeed = maxSpeed;
		} else if (newBallSpeed < minSpeed) {
			currentSpeed = minSpeed;
		} else {
			currentSpeed = newBallSpeed;
		}

		Debug.Log ("NEW BALL SPEED = " + currentSpeed);
	}
	*/


	public void SetPlayerHitBall (bool ballHitByPlayer){
		playerHitBall = ballHitByPlayer;		
	}

	public bool GetPlayerHitBall(){
		return playerHitBall;
	}


}
