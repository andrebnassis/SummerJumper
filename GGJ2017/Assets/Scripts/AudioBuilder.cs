using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBuilder : MonoBehaviour
{


    public AudioClip[] audioFiles;
    public AudioSource audio;
    bool play = false;
    public int rand = 0;
    // Use this for initialization
    void Start()
    {

        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!audio.isPlaying)
        {
            rand = Random.Range(GetComponent<GameManagerSettings>().GetLevel() - 3, GetComponent<GameManagerSettings>().GetLevel());
            audio.PlayOneShot(audioFiles[rand]);
        }
    }
}
