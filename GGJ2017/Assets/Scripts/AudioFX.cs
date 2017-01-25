using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : MonoBehaviour {

    public AudioSource AudioSource;

    public AudioClip Charging;
    public AudioClip Jumping;
    public AudioClip Idle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayCharging()
    {
        AudioSource.clip = Charging;
        AudioSource.timeSamples = 0;
        AudioSource.loop = true;
        AudioSource.Play();
    }

    public void StopCharging()
    {
        AudioSource.Stop();
    }

    public void PlayJump()
    {
        AudioSource.PlayOneShot(Jumping);
    }

    public void PlayIdle()
    {
        AudioSource.PlayOneShot(Idle);
    }

}
