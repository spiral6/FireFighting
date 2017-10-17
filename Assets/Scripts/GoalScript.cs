using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalScript : MonoBehaviour {

    public static Canvas goalMenu;
    public Button MenuButton;

	// Use this for initialization
	void Start () {
        goalMenu = GameObject.Find("fireSprite/Main Camera/GoalMenu").GetComponent<Canvas>();
        goalMenu.gameObject.SetActive(false);
        MenuButton.onClick.AddListener(Quit);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void GoalScreen()
    {
        Cursor.visible = true;
        goalMenu.gameObject.SetActive(true);
    }

    void Quit()
    {
        SceneManager.LoadScene("Menu");
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex-1);
    }
}
