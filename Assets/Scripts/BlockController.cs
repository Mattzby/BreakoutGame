using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
	public Color blockColor;
	public float ballSpeedModifier;
	public int successScore;
	public int failScore;

	private SpriteRenderer spriteRender;

	void Start(){
		spriteRender = GetComponent<SpriteRenderer> ();
		spriteRender.color = blockColor;
	}

	void OnCollisionEnter2D(Collision2D collisionInfo) {
		if (collisionInfo.gameObject.CompareTag("Ball")) {
			BallController ballController = collisionInfo.gameObject.GetComponent<BallController>();
			Rigidbody2D ballBody = collisionInfo.gameObject.GetComponent<Rigidbody2D> ();
			Color ballColor = collisionInfo.gameObject.GetComponent<SpriteRenderer> ().color;

			int score = GameController.getInstance ().GetScore();
			//int newScore;

			//Check if colors match, adjust score accordingly
			if (ballColor.Equals(blockColor)) {
				score = score + successScore;

				//if the player aimed the ball, adjust the speed
				if (ballController.GetPlayerHitBall() == true) {
					ballController.AdjustBallSpeedModifier (-ballSpeedModifier);
					ballBody.velocity = ballBody.velocity * ballController.GetBallSpeedModifier ();
					//Debug.Log ("BALL SPEED MODIFIER DECREASED TO " + ballController.GetBallSpeedModifier());
				}

				//destroy block
				Destroy(gameObject);

				//Check if all blocks destroyed, if so, move to next scene
				if (AllBlocksDestroyed ()) {
					GameController.getInstance ().NextScene();
				}
			} 
			else {
				//if the player aimed the ball, subtract score, adjust the speed
				if (ballController.GetPlayerHitBall() == true) {
					score = score + failScore;
					ballController.AdjustBallSpeedModifier (ballSpeedModifier);
					ballBody.velocity = ballBody.velocity * ballController.GetBallSpeedModifier ();
					//Debug.Log ("BALL SPEED MODIFIER INCREASED TO " + ballController.GetBallSpeedModifier());
				}
			}

			//update score
			GameController.getInstance ().SetScore (score);

			//change ball's color
			ballController.ChangeColor();

			//update ball hit by player flag - since ball hit block
			ballController.SetPlayerHitBall (false);
		}
			
	}

	//Checks to see if all blocks are destroyed, returns true if all blocks destroyed
	private bool AllBlocksDestroyed() {
		GameObject[] blocks = GameObject.FindGameObjectsWithTag ("Block");
		if (blocks.Length == 1) {
			return true;
		} else {
			return false;
		}		
	}
}
