using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataController : MonoBehaviour {

    public PlayerProgress playerProgress;
    public Text CanvasText;
    void Awake()
    {
       
        LoadPlayerProgress();


    }

	// Use this for initialization
	void Start () {
        ////Debug.Log("oi");
        ////Debug.Log(GetHighestPlayerScore().ToString());
        try { 
        CanvasText.text = "Best: " + GetHighestPlayerScore().ToString();
        }
        catch(Exception e)
        {

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();

        if (PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
            
        }
        
    }

    public void SubmitNewPlayerScore(int newScore)
    {
        if(newScore > playerProgress.highestScore)
        {
            playerProgress.highestScore = newScore;
            SavePlayerProgress();
        }
    }

    public int GetHighestPlayerScore()
    {
        return playerProgress.highestScore;
    }

    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("highestScore",playerProgress.highestScore);
    }
}
