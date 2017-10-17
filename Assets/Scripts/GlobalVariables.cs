using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables: MonoBehaviour {

    public static float player_health;
    public static bool goalReached;
    public static float lifeTime;
    public static float lifeTimeLimit;

    // Use this for initialization
    void Start () {
        Init();
	}

    public static void Init()
    {
        player_health = 1;
        goalReached = false;
        lifeTime = 60.0f;
        lifeTimeLimit = Time.time + lifeTime;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
