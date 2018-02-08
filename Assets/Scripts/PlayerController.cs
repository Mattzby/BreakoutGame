using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;
	public GameObject ballPrefab;
	public Transform ballSpawn;

	private Rigidbody2D rb;
	private bool ballAttached;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		SpawnBall();	
	}
	
	// Update is called once per frame
	void Update () {
		ShootBall();		
	}

	void FixedUpdate() {
		Vector2 movement = new Vector2 ();

		//TODO: Add device detection to support both mobile and web/standalone
		//Touch Controls
		if (Input.touchCount > 0) {
			Vector2 touchPixelPosition = Input.GetTouch (0).position;
			Vector2 touchWorldPosition = Camera.main.ScreenToWorldPoint (touchPixelPosition);

			Debug.Log("TOUCH PIXEL POSITION X = " + touchPixelPosition);
			Debug.Log ("TOUCH WORLD POSITION X = " + touchWorldPosition);
			Debug.Log ("PLAYER OBJECT POSITION X = " + rb.position.x);

			if (touchWorldPosition.x > rb.position.x) {
				Debug.Log ("MOVE PLAYER RIGHT");
				movement = new Vector2 (1f, 0.0f); 
			} else if (touchWorldPosition.x < rb.position.x) {
				Debug.Log ("MOVE PLAYER LEFT");
				movement = new Vector2 (-1f, 0.0f); 
			}
		}
			
		//Keyboard Controls
		//float moveHorizontal = Input.GetAxis("Horizontal");
		//movement = new Vector2 (moveHorizontal, 0.0f);

		rb.velocity = movement * movementSpeed;

		//TODO: Add boundary positions to restrict player movement
		float xPosition = Mathf.Clamp (rb.position.x, -7.5f, 7.5f);
		rb.position = new Vector2 (xPosition,0.0f);




	}

	//Spawns ball above player paddle
	public void SpawnBall() {
		//Instantiate ball at position just above player paddle - increased y value
		GameObject ball;
		ball = Instantiate (ballPrefab, ballSpawn.position, transform.rotation);

		//Make ball a child of player paddle - so that it moves with parent
		ball.transform.parent = transform;

		//Change body type of ball so that it moves same as parent (player paddle)
		Rigidbody2D ballBody = ball.GetComponent<Rigidbody2D> ();
		if (!ballBody.isKinematic) {
			ballBody.bodyType = RigidbodyType2D.Kinematic;		
		}
	 	ballAttached = true;
	}

	void ShootBall() {
		//destroy spawned ball, instantiate new ball gameobject - 
		//resets any scale issues encountered by parent and reverts body type change


		if (ballAttached && Input.GetButton ("Jump") || ballAttached && Input.touchCount > 1) {
			ballAttached = false;
			GameObject ball;
			ball = Instantiate (ballPrefab, ballSpawn.transform.position, ballSpawn.transform.rotation);

			//find child that is tagged as "Ball" and destroy it
			foreach (Transform child in transform) {
				if (child.CompareTag("Ball")) 
					{
						Destroy(child.gameObject);
					}				
			}
			ball.GetComponent<Rigidbody2D>().velocity = Vector2.up * ball.GetComponent<BallController>().speed;
		}
	}

}
