using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainPoint : MonoBehaviour {

    public GameManager gameManager;
    public Player player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!player.HitHead && !player.isInvulnerable)
        {
            gameManager.AddScore();
        }
    }
}
