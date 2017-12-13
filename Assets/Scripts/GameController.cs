using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public Text scoreText;
	public Text livesText;
	public Text levelNameText;
	public Text speedText;

	private static GameController instance;
	private int score;
	private int lives;
	private float ballSpeedPercentage;

	void Awake ()
	{
		if (instance == null){
			instance = this;
			DontDestroyOnLoad(gameObject);
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
		else if (instance != this){
			Destroy (gameObject);
		}
	}

	/*void OnEnable() 
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}*/
		
	void Start () 
	{
		ballSpeedPercentage = 0;
		score = 0;
		lives = 3;	
		UpdateScore ();
		UpdateLives ();
		UpdateLevelName();
		UpdateBallSpeedText();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	void UpdateLives()
	{
		livesText.text = "Lives: " + lives;
	}

	void UpdateLevelName()
	{
		levelNameText.text = SceneManager.GetActiveScene().name;
	}

	public void SetBallSpeedText(float ballSpeed) {
		ballSpeedPercentage = 100 * ballSpeed;
		UpdateBallSpeedText ();
	}

	void UpdateBallSpeedText()
	{
		speedText.text = "Speed: " + ballSpeedPercentage + "%";	
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

		if (lives < 1) {
			SceneManager.LoadScene ("Game Over", LoadSceneMode.Single);
		} else {
			UpdateLives ();
		}
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
		int nextSceneIndex = SceneManager.GetActiveScene ().buildIndex + 1;
		if (SceneManager.sceneCountInBuildSettings > nextSceneIndex) {
			SceneManager.LoadScene (nextSceneIndex);
		}
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){

		//if not the title screen or game over screen
		if (!scene.name.Equals ("Title") && !scene.name.Equals ("Game Over")) {
			
			scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();
			livesText = GameObject.Find ("LivesText").GetComponent<Text> ();
			levelNameText = GameObject.Find ("LevelNameText").GetComponent<Text> ();
			speedText = GameObject.Find ("SpeedText").GetComponent<Text> ();

			UpdateScore ();
			UpdateLives ();
			UpdateLevelName();
			UpdateBallSpeedText();
		}
	}		
}
