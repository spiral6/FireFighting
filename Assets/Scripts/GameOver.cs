using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour {

    public Button MainMenuButton;
    public static Animator death_animator;

	// Use this for initialization
	void Start () {
        MainMenuButton = MainMenuButton.GetComponent<Button>();
        MainMenuButton.onClick.AddListener(MainMenu);
        death_animator = GameObject.Find("sprite_char_death_0").GetComponent<Animator>();
        Debug.Log(death_animator.GetComponent<SpriteRenderer>().sprite.name);
        Init();
	}

    public static void Init()
    {
        Cursor.visible = true;
    }
	
    void MainMenu()
    {
        Debug.Log("Loading main menu.");
        SceneManager.LoadScene("Menu");
    }

	// Update is called once per frame
	void Update () {
        //play death sprite
    }
}
