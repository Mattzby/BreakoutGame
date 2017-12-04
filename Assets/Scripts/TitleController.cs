using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("Fire1")) {

			//load into test level
			//SceneManager.LoadScene ("Level Test", LoadSceneMode.Single);

			//load into level 1
			SceneManager.LoadScene ("Level 1", LoadSceneMode.Single);			
		}
		
	}
}
