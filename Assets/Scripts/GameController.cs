using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public Text scoreText;
	public Text livesText;

	private static GameController instance;
	private int score;
	private int lives;

	void Awake ()
	{
		instance = this;

		//TODO: Is this needed only for dev? Or should each scene have a gameController
		//look to see if a GameController already exists - if so, destroy this one
		GameObject[] otherGameController = GameObject.FindGameObjectsWithTag("GameController");
		if (otherGameController.Length == 1){
			DontDestroyOnLoad (transform.gameObject);
			//DontDestroyOnLoad (scoreText.transform.gameObject);
			//DontDestroyOnLoad (livesText.transform.gameObject);
		} else {
			Destroy (gameObject);
			//Destroy (scoreText.transform.gameObject);
			//Destroy (livesText.transform.gameObject);
		}
	}

	void OnEnable() 
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}


	void Start () 
	{
		score = 0;
		lives = 3;

		UpdateScore ();
		UpdateLives ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + GameController.getInstance().GetScore();
	}

	void UpdateLives()
	{
		livesText.text = "Lives: " +  GameController.getInstance().GetLives();
	}

	public void SetScore (int newScoreValue)
	{
		score = newScoreValue;
		UpdateScore ();
	}

	public int GetScore()
	{
		return score;
	}

	public void SetLives (int newLivesValue)
	{
		lives = newLivesValue;

		//TODO: Add gameover screen with restart game functionality
		if (lives < 0) {
			SceneManager.LoadScene ("Game Over", LoadSceneMode.Single);
		}
		UpdateLives ();
	}

	public int GetLives ()
	{
		return lives;
	}

	public static GameController getInstance()
	{
		return instance;
	}

	public void NextScene() {
		//TODO: Why the fuck can't unity load a scene by index properly?
		Debug.Log ("MOVING TO NEXT SCENE!");
		//Debug.Log ("SCENE INDEX = " + nextSceneIndex);

		/*if (SceneManager.GetSceneByName ("Level " + levelNumber).Equals(null)) {
			SceneManager.LoadScene ("Game Over", LoadSceneMode.Single);
		} 
		else {
			SceneManager.LoadScene("Level " + GameController.getInstance().levelNumber, LoadSceneMode.Single);
			GameController.getInstance().levelNumber = GameController.getInstance().levelNumber + 1;
		}*/
		int nextSceneIndex = SceneManager.GetActiveScene ().buildIndex + 1;
		if (SceneManager.sceneCountInBuildSettings > nextSceneIndex) {
			SceneManager.LoadScene (nextSceneIndex);
		}		
	}

	//TODO: Is updating the reference to the new UI text the best way? Create static UI GameObjects/Scripts?
	//Reattach score/life text references after new scene loads
	void OnSceneLoaded(Scene scene, LoadSceneMode mode){

		//if not the title screen or game over screen
		if (!scene.name.Equals ("Title") && !scene.name.Equals ("Game Over")) {
			//wtf is going on here?
			GameController.getInstance ().SetScore (score);
			GameController.getInstance ().SetLives (lives);

			scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();
			livesText = GameObject.Find ("LivesText").GetComponent<Text> ();

			UpdateScore ();
			UpdateLives ();
		}
	}

	void resetStats(){
		
	}

		
}
