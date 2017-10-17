using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

	bool PausedGame = false;

	public Canvas PauseMenu;
	public Button ResumeButton;
	public Button QuitButton;
	public Button RestartButton;
	GameObject[] pauseObjects;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f;
		PauseMenu = PauseMenu.GetComponent<Canvas> ();
		ResumeButton = ResumeButton.GetComponent<Button> ();
		QuitButton = QuitButton.GetComponent<Button> ();
		RestartButton = RestartButton.GetComponent<Button> ();
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		/*foreach(GameObject gameobject in pauseObjects){
			Debug.Log (gameobject.ToString());
		}*/
		Cursor.visible = false;

		ResumeButton.onClick.AddListener (Resume);
		QuitButton.onClick.AddListener (Quit);
		RestartButton.onClick.AddListener (Reload);
		hidePaused();
	}

	void Resume(){
		Cursor.visible = false;
		hidePaused ();
		Time.timeScale = 1.0f;
		PausedGame = false;
	}

	void Quit(){
        Time.timeScale = 1.0f;
        SceneManager.LoadScene ("Menu");
	}

	void Reload(){
        GlobalVariables.Init();
        SceneManager.LoadScene(SceneManager.GetActiveScene ().name);
	}

	void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}

	void showPaused(){
		foreach (GameObject g in pauseObjects) {
			g.SetActive (true);
		}
		PausedGame = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("escape")) {
			if (PausedGame == false) {
				Time.timeScale = 0.0f;
				Cursor.visible = true;
				showPaused ();
			} else {
				Resume ();
			}
		}
	}
}
