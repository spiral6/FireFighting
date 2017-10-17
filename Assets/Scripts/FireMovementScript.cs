using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireMovementScript : MonoBehaviour 
{
	public float fireSpeed;
    
	public Rigidbody2D fireBody;
    private float cameraInitialY;
    
    //public bool facingRight = true;
    public float jumpPower;



	// Use this for initialization
	void Start () 
	{
        fireBody = gameObject.GetComponent<Rigidbody2D>();
        
        
        //touchingGround = true;
        //cameraInitialY = Camera.main.transform.position.y;
    }


	
	// Update is called once per frame
	void Update () 
	{
        if(GlobalVariables.player_health <= 0 || GlobalVariables.goalReached)
        {
            fireBody.velocity = new Vector2(0,0);
        }
        else
        {
            PlayerMove();
        }
	}

	void PlayerMove ()
	{

		float moveY = Input.GetAxis ("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        /*if(moveX < 0.0f && facingRight == false)
        {
            flipPlayer();
        }else if(moveX > 0.0f && facingRight == true)
        {
            flipPlayer();
        }*/

		//fireBody.velocity = new Vector2 (fireSpeed, fireSpeed * moveY);
		fireBody.velocity = new Vector2 (fireSpeed * moveX, fireBody.velocity.y);
	}

    void Jump()
    {
        fireBody.AddForce(Vector2.up * jumpPower);
    }

  

    /*void flipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localscale = gameObject.transform.localScale;
        localscale *= -1;
        transform.localScale = localscale;
    }*/



}
