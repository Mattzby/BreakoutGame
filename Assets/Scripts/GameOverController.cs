using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {
	public Text scoreText;
	public Text instructionsText;

	private int playerScore;

	// Use this for initialization
	void Start () {

		//get score from gamecontroller and display
		if (GameController.getInstance () != null) {
			playerScore = GameController.getInstance ().GetScore ();
		}
		else {
			playerScore = 0;
		}
		scoreText.text = "Score: " + playerScore;

		//Cleanup GameController to reset score/lives for next playthrough
		//Destroy (GameObject.FindGameObjectWithTag("GameController"));
	}

	// Update is called once per frame
	void Update () {
		bool highScoreInputActivated = false;
		//bool highScoreInputActivated = GameObject.Find("HighScoreTable").GetComponent<HighScoreController>().PlayerIsTyping();

		if (Input.GetButton ("Fire1") && !highScoreInputActivated) {
			SceneManager.LoadScene ("Title", LoadSceneMode.Single);			
		}	
	}
}
