using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour {

	public GameObject highScoreRow;
	public GameObject scoreText;

	private List<HighScore> highScores;
	private int playerScore;
	private bool nameEntryActivated;
	//private bool highScoreAchieved;

	void Start () {
		nameEntryActivated = false;
		highScores = new List<HighScore> ();

		//prepopulated highscores
		highScores.Add(new HighScore("MRC",70000));
		highScores.Add(new HighScore("LOL",50000));
		highScores.Add(new HighScore("WTF",45000));
		highScores.Add(new HighScore("XYZ",30000));
		highScores.Add(new HighScore("NUB",10000));

		if (GameController.getInstance () != null) {
			playerScore = GameController.getInstance ().GetScore ();
		}
		else {
			playerScore = 0;
		}

		EvaluatePlayerScore(playerScore);
		GenerateHighScoreRows();

		//Cleanup GameController to reset score/lives for next playthrough
		Destroy (GameObject.FindGameObjectWithTag("GameController"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateHighScoreRows() {	
		int rank = 1;
		foreach (HighScore hs in highScores) {
			if (rank < 6) {
				GameObject currentRow = Instantiate (highScoreRow, transform) as GameObject;
				Text rankText = currentRow.transform.GetChild (0).GetComponent<Text>();
				Text nameText = currentRow.transform.GetChild (1).GetComponent<Text>();
				Text scoreText = currentRow.transform.GetChild (2).GetComponent<Text>();

				rankText.text = rank.ToString();
				scoreText.text = hs.score.ToString();
				
				//check if null - if so, that means new highscore and requires player input
				if (hs.initials != null) {
					nameText.text = hs.initials;
				} else {
					//TODO: Allow user to input name/initials when achieving high score
					nameText.text = "YOU";
					rankText.color = Color.red;
					scoreText.color = Color.red;
					nameText.color = Color.red;
					//nameText.gameObject.GetComponent<Text> ().enabled = false;
					//nameText.gameObject.GetComponent<InputField> ().enabled = true;
				}
			}
			rank++;			
		}
	}

	bool EvaluatePlayerScore(int playerScore) {
		int index = 0;
		foreach (HighScore hs in highScores) {
			if (playerScore > hs.score) {
				highScores.Insert(index, new HighScore(null, playerScore));
				return true;
			}
			index++;
		}
		return false;
	}

	//Class that stores existing high scores
	private class HighScore {
		public string initials;
		public int score;

		public HighScore(string i, int s){
			initials = i;
			score = s;
		}
	}

	public bool PlayerIsTyping() {
		return nameEntryActivated;
	}
}
