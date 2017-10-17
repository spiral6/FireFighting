using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Canvas Canvas;
	public Button StartButton;
	public Button QuitButton;
    public Camera MainCamera;
    public AudioSource menumusicsource;
    private bool CameraMoved;

	// Use this for initialization
	void Start () {
		Canvas = Canvas.GetComponent<Canvas> ();
		StartButton = StartButton.GetComponent<Button> ();
		StartButton.onClick.AddListener (StartGame);
		QuitButton = QuitButton.GetComponent<Button> ();
		QuitButton.onClick.AddListener (QuitGame);
        MainCamera = MainCamera.GetComponent<Camera>();
        CameraMoved = false;
        menumusicsource = GetComponent<AudioSource>();
        menumusicsource.Play();
	}
	void StartGame(){
        Debug.Log("Loading level 1.");
        StartCoroutine(StartCameraMove());
        StartCoroutine(InitializeGame());
        //Debug.Log(GlobalVariables.player_health + " " + GlobalVariables.goalReached);
    }

    IEnumerator StartCameraMove()
    {
        while (MainCamera.transform.position.y > -14.75f)
        {
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y - .75f, MainCamera.transform.position.z);
            yield return new WaitForSeconds(0.05f);
        }

        CameraMoved = true;
    }
    IEnumerator InitializeGame()
    {
        while (!CameraMoved)
        {
            yield return new WaitForSeconds(0.1f);
        }
        GlobalVariables.Init();
        FireBehaviorScript.resetLifeTimer();
        SceneManager.LoadScene("Level1");
        MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y + 14.75f, MainCamera.transform.position.z);
        CameraMoved = false;
    }

    void QuitGame(){
		Debug.Log ("You pressed the quit button.");
		Application.Quit ();
	}

	

}
