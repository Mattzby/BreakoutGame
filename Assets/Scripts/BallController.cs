using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	// Movement Speed
	public float speed;
	//Pass in color list from variable set in level settings
	public Color[] colors;

	private SpriteRenderer ballSprite;
	private int currentColorIndex;
	private float bonusSpeedModifier;
	//private Color currentColor;

	// Use this for initialization
	void Start () {
		bonusSpeedModifier = 1;
		ballSprite = GetComponent<SpriteRenderer> ();
		currentColorIndex = 0;

		//remove code when list can be passed in
		//colorList.Add(Color.red);
		//colorList.Add(Color.green);
		//colorList.Add(Color.cyan);

		//set initial ball color
		ballSprite.color = colors [currentColorIndex];
	}

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
			// Calculate hit Factor
			float x = hitFactor (transform.position,
				         col.transform.position,
				         col.collider.bounds.size.x);

			// Calculate direction, set length to 1
			Vector2 dir = new Vector2 (x, 1).normalized;

			// Set Velocity with dir * speed
			GetComponent<Rigidbody2D> ().velocity = dir * speed * bonusSpeedModifier;
		} else if (!col.gameObject.CompareTag("Border")){
			//ballSprite.color = new Color (Random.Range(0,255),Random.Range(0,255),Random.Range(0,255));
			//ChangeColor();
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

	//TODO: Speed modifier needs to be added when ball hits block, not just when hitting player 
	public void AdjustSpeedModifier (float modifyBy) {
		bonusSpeedModifier = bonusSpeedModifier + modifyBy;
	}

	//public Color GetCurrentColor(){
	//	return currentColor;
	//}

}
