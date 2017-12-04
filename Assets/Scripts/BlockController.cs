using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
	public Color blockColor;
	public float ballSpeedModifier;

	private SpriteRenderer spriteRender;

	void Start(){
		spriteRender = GetComponent<SpriteRenderer> ();
		spriteRender.color = blockColor;
	}

	void OnCollisionEnter2D(Collision2D collisionInfo) {

		if (collisionInfo.gameObject.CompareTag("Ball")) {
			Color ballColor = collisionInfo.gameObject.GetComponent<SpriteRenderer> ().color;
			int oldScore = GameController.getInstance ().GetScore();
			int newScore;

			//Check if colors match, adjust score accordingly
			if (ballColor.Equals(blockColor)) {
				newScore = oldScore + 1000;
				collisionInfo.gameObject.GetComponent<BallController> ().AdjustSpeedModifier (ballSpeedModifier);
				Destroy(gameObject);
			
				//Check if all blocks destroyed, if so, move to next scene
				if (AllBlocksDestroyed ()) {
					GameController.getInstance ().NextScene();
				}
			} 
			else {
				newScore = oldScore - 100;
				collisionInfo.gameObject.GetComponent<BallController> ().AdjustSpeedModifier (-ballSpeedModifier);
			}

			GameController.getInstance ().SetScore (newScore);
			//change ball's color
			collisionInfo.gameObject.GetComponent<BallController>().ChangeColor();

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
