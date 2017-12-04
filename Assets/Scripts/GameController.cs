using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public Text scoreText;
	public Text livesText;

	private static GameController instance;
	private int score;
	private int lives;
	private bool isGameOver;

	void Awake ()
	{
		instance = this;
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
		scoreText.text = "Score: " + score;
	}

	void UpdateLives()
	{
		livesText.text = "Lives: " + lives;
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
		if (lives == 0) {
			isGameOver = true;
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
}
