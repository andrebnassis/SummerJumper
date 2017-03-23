using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpBarProgress : MonoBehaviour {

    public GameObject player;
	private float power;

    // Use this for initialization
	void Start () {
        power = 0;
        player = GameObject.Find("Kid");
	}
	
	// Update is called once per frame
	void Update () {
        power = player.GetComponent<Player>().Acumulate;
        GetComponent<Image>().color = new Color(1, 1 - power*5/100, 1 - power*5/100, 0.3f);

        
	}
}
