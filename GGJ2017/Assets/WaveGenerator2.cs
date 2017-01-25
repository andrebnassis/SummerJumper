using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator2 : MonoBehaviour {

    public GameObject Wave;


    private float currentPitchValue;
    private float timeInterval = 2.5f;
    private float period = 0.0f;

    // Use this for initialization
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        period += Time.fixedDeltaTime;
        if(period >= timeInterval)
        {
            CreateWave();
            period = 0;
        }

    }

    public void CreateWave()
    {
        GameObject tmp = Instantiate(Wave, transform.position, Wave.transform.rotation);
        tmp.transform.parent = null;
    }

    public void CreateWave(float pitchValue)
    {
        GameObject tmp = Instantiate(Wave, transform.position, Wave.transform.rotation);
        tmp.transform.parent = null;
       
    }

}
