using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	// Movement Speed
	public float speed;
	//Pass in color list from variable set in level settings
	public List<Color> colorList;

	private SpriteRenderer ballSprite;
	//private Color currentBallColor;


	// Use this for initialization
	void Start () {
		ballSprite = GetComponent<SpriteRenderer> ();

		//remove code when list can be passed in
		colorList.Add(Color.red);
		colorList.Add(Color.green);
		colorList.Add(Color.cyan);
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
			GetComponent<Rigidbody2D> ().velocity = dir * speed;
		} else if (!col.gameObject.CompareTag("Border")){
			//ballSprite.color = new Color (Random.Range(0,255),Random.Range(0,255),Random.Range(0,255));
			ChangeColor();
		}
	}

	void ChangeColor () {
		//Color oldBallColor;

		//if (currentBallColor != Color.white) {
		//	oldBallColor = currentBallColor;
		//}

		int randomIndex = Random.Range(0,colorList.Count);
		ballSprite.color = colorList[randomIndex];

		//Store the color so that it may be removed
		//currentBallColor = colorList[randomIndex];
		//colorList.RemoveAt[randomIndex];
		//colorList.Add [oldBallColor];

	}
}
