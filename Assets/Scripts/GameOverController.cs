using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {
	public Text scoreText;

	// Use this for initialization
	void Start () {
		int score = GameController.getInstance ().GetScore ();
		scoreText.text = "Score: " + score;	

		//Cleanup GameController to reset score/lives for next playthrough
		Destroy (GameObject.FindGameObjectWithTag("GameController"));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")) {
			SceneManager.LoadScene ("Title", LoadSceneMode.Single);			
		}	
	}
}
