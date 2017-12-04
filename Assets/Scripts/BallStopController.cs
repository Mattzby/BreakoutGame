using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStopController : MonoBehaviour {
	
	public GameObject player;

	void OnCollisionEnter2D(Collision2D collisionInfo) {

		//Update life counter if object was ball
		if(collisionInfo.gameObject.CompareTag("Ball")){
			int oldLives = GameController.getInstance().GetLives();
			int newLives = oldLives - 1;
			GameController.getInstance ().SetLives (newLives);

			//If player has lives, spawn a new ball
			//TODO: Remove this logic when actual gameover logic is created.
			if (newLives > 0) {
				player.GetComponent<PlayerController> ().SpawnBall ();
			}

			Destroy(collisionInfo.gameObject);
		}

	}
}
