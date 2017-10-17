using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FireBehaviorScript : MonoBehaviour {

    public bool dead;
    public SpriteRenderer fireSprite;
    public Animation anim;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public SpriteRenderer gasfire;
    
    public bool player_invincible;
    public float invincibilityTime;

    public Image BlackScreen;
    public Image WhiteScreen;
    

    // Use this for initialization
    void Start () {
        dead = false;
        anim = GetComponent<Animation>();
        GlobalVariables.player_health = 1;
        player_invincible = false;
        invincibilityTime = 0;
        BlackScreen = BlackScreen.GetComponent<Image>();
        WhiteScreen = WhiteScreen.GetComponent<Image>();
        resetLifeTimer();
        gasfire.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(GlobalVariables.lifeTimeLimit + " " + GlobalVariables.lifeTime);
        lifeTimer();
        InviniciblityTimer(player_invincible);

        if (!player_invincible)
        {
            if (GlobalVariables.player_health < 1)
            {
                dead = true;
            }

            if (GlobalVariables.player_health > 3)
            {
                GlobalVariables.player_health = 3;
            }

            if (dead)
            {
                Debug.Log("The fire is out.");
                StartCoroutine(FadeImage(true));
                Debug.Log("Black screen active.");
            }
        }
    }

    void lifeTimer()
    {
        if(Time.time > GlobalVariables.lifeTimeLimit && !GlobalVariables.goalReached)
        {
            GlobalVariables.player_health -= 1;
            UpdatePlayerSprite(false);
            GlobalVariables.lifeTimeLimit = Time.time + GlobalVariables.lifeTime;
        }
    }

    public static void resetLifeTimer()
    {
        GlobalVariables.lifeTimeLimit = Time.time + GlobalVariables.lifeTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!player_invincible)
        {
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                GlobalVariables.player_health -= 1;
                if(!(GlobalVariables.player_health <= 0))
                {
                    //make smaller sprite
                    //play hissing sound effect
                    //flash white
                    player_invincible = true;
                    invincibilityTime = Time.time + 2.0f;
                    UpdatePlayerSprite(false);
                    resetLifeTimer();
                    StartCoroutine(InvincibilityRendering());
                    collision.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                }
            }
        }
        if (collision.gameObject.tag.Equals("Flammable"))
        {
            GlobalVariables.player_health += 1;
            Debug.Log(GlobalVariables.player_health);
            //flash orange
            UpdatePlayerSprite(true);
            resetLifeTimer();
            //set collision object on fire, disable.
            collision.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            //make bigger sprite.
            //play flame sound effect
        }
        if (collision.gameObject.tag.Equals("Goal"))
        {
            GlobalVariables.goalReached = true;
            Debug.Log("Level complete.");
            StartCoroutine(FadeImageWhite(true));
            
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            
        }
    }

    void InviniciblityTimer(bool player_invincible)
    {
        if (player_invincible)
        {
            if(Time.time >= invincibilityTime)
            {
                this.player_invincible = false;
            }
        }
    }

    void UpdatePlayerSprite(bool growth)
    {
        var animator = gameObject.GetComponent<Animator>();
        switch ((int)GlobalVariables.player_health)
        {
            case 1:
                fireSprite.sprite = sprite1;
                fireSprite.GetComponent<BoxCollider2D>().edgeRadius = 0.0f;
                animator.SetTrigger("mediumToSmall");
                break;
            case 2:
                if (growth)
                {
                    fireSprite.sprite = sprite2;
                    fireSprite.GetComponent<BoxCollider2D>().edgeRadius = 0.0f;
                    animator.SetTrigger("smallToMedium");
                }
                else
                {
                    fireSprite.sprite = sprite2;
                    fireSprite.GetComponent<BoxCollider2D>().edgeRadius = 0.0f;
                    animator.SetTrigger("largeToMedium");
                }
                break;
            case 3:
                fireSprite.sprite = sprite3;
                fireSprite.GetComponent<BoxCollider2D>().edgeRadius = 1.2f;
                animator.SetTrigger("mediumToLarge");
                break;
            default:
                break;
        }
    }


    private IEnumerator InvincibilityRendering()
    {
        while (player_invincible)
        {
            SpriteRenderer smr = gameObject.GetComponent<SpriteRenderer>();
            smr.enabled = false; //This is where I want to disable all the Sprite Mesh Renderers...
            yield return new WaitForSeconds(.1f);
            smr.enabled = true; //...and this is where I want to re-enable them.
            yield return new WaitForSeconds(.1f);
            resetLifeTimer();
        }
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                BlackScreen.color = new Color(1, 1, 1, i);
                yield return null;
            }
            Debug.Log("Load game over scene.");
            GameOver.Init();
            //GameOver.death_animator.Play("death", -1, 0.0f);
            SceneManager.LoadScene("GameOver");

        }
    }

    IEnumerator FadeImageWhite(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                WhiteScreen.color = new Color(255, 255, 255, i);
                yield return null;
            }
            fireSprite.enabled = false;
            gasfire.enabled = true;
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                WhiteScreen.color = new Color(255, 255, 255, i);
                yield return null;
            }
            GoalScript.GoalScreen();


            //GameOver.death_animator.Play("death", -1, 0.0f);
        }
    }

}
