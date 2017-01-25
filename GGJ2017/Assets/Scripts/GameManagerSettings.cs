using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSettings : MonoBehaviour {


    public int level;
    public float DistanceToGroundToNextWave = 2f;
    public float WaveInterval = 2.0f;
    private float time;
    public bool GameOver = false;

    
    private void Awake()
    {
       
    }
    // Use this for initialization
    void Start () {
        level = 3;
	}
	
	// Update is called once per frame
	void Update () {
        
        time += Time.fixedDeltaTime;
        time = time % 40;
        if(time > 10f && time < 15f)
        {
            level = 5;
        }
        else if(time >= 15f && time < 25f)
        {
            level = 7;
        }	
        else if(time > 25f)
        {
            level = 9;
        }
	}

    public int GetLevel()
    {
        return level;
    }
}
