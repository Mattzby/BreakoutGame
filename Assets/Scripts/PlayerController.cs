using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;
	public GameObject ballPrefab;
	public Transform ballSpawn;

	private Rigidbody2D rb;
	private bool ballAttached;
	private GameObject spawnedBall;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		spawnedBall = SpawnBall();	
	}
	
	// Update is called once per frame
	void Update () {
		ShootBall();		
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = new Vector3 (moveHorizontal, 0.0f);

		rb.velocity = movement * movementSpeed;
	}

	//Spawns ball above player paddle
	private GameObject SpawnBall() {
		//Instantiate ball at position just above player paddle - increased y value
		Vector3 ballSpawn = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
		GameObject ball;
		ball = Instantiate (ballPrefab, ballSpawn, transform.rotation);

		//Make ball a child of player paddle - so that it moves with parent
		ball.transform.parent = transform;

		//Change body type of ball so that it moves same as parent (player paddle)
		Rigidbody2D ballBody = ball.GetComponent<Rigidbody2D> ();
		if (!ballBody.isKinematic) {
			ballBody.bodyType = RigidbodyType2D.Kinematic;		
		}

	 	ballAttached = true;
		return ball;
	}

	void ShootBall() {
		//destroy spawned ball, instantiate new ball gameobject - 
		//resets any scale issues encountered by parent and reverts body type change
		if (ballAttached && Input.GetButton ("Fire1")) {
			ballAttached = false;
			GameObject ball;
			ball = Instantiate (ballPrefab, spawnedBall.transform.position, spawnedBall.transform.rotation);
			Destroy (spawnedBall);

			ball.GetComponent<Rigidbody2D>().velocity = Vector2.up * ball.GetComponent<BallController>().speed;
		}

	}

}
