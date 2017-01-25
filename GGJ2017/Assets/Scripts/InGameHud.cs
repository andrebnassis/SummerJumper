using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameHud : MonoBehaviour {

    public GameManager gameManager;
    public Text countWaves;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       

    }

    void OnGUI()
    {
        if (countWaves != null)
        {
            countWaves.text = gameManager.GetScore() + "";
        }
    }
}
