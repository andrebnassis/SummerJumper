using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{

    public GameObject Wave;
    private GameObject GameManager;
    //private float timeInterval;
    //private float period;
    public Vector3[] WaveSize;

    private float currentPitchValue;

    // Use this for initialization
    void Start()
    {

        //WaveSize = new Vector3[3] {
        //    new Vector3(1,1,1),
        //    new Vector3(2,1,2),
        //    new Vector3(3,1,3) };
        GameManager = GameObject.Find("GameManager");
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateWave()
    {
        GameObject tmp = Instantiate(Wave, transform.position, Wave.transform.rotation);
        tmp.transform.localScale = GetNewScale();
        tmp.transform.parent = null;
    }

    public void CreateWave(float pitchValue)
    {
        GameObject tmp = Instantiate(Wave, transform.position, Wave.transform.rotation);
        tmp.transform.localScale = GetNewScale(pitchValue);
        tmp.transform.parent = null;
        //period = 0;
    }

    public Vector3 GetNewScale()
    {
        var scale = new Vector3();

        var pitchValue = GameManager.GetComponent<MusicDecoder>().pitchValue;
        var MaxPitchValue = GameManager.GetComponent<MusicDecoder>().max_pitchValue;
        var MinPitchValue = GameManager.GetComponent<MusicDecoder>().min_pitchValue;

        var range = (MaxPitchValue - MinPitchValue) / WaveSize.Length;
        if (pitchValue <= MinPitchValue + range)
        {
            scale = WaveSize[0];
        }
        else if (pitchValue > MinPitchValue + range && pitchValue < MinPitchValue + 2 * range)
        {
            scale = WaveSize[1];
        }
        else
        {
            scale = WaveSize[2];
        }

        return scale;
    }

    public Vector3 GetNewScale(float pitchValue)
    {
        var scale = new Vector3();

        var MaxPitchValue = GameManager.GetComponent<MusicDecoder>().max_pitchValue;
        var MinPitchValue = GameManager.GetComponent<MusicDecoder>().min_pitchValue;

        var range = (MaxPitchValue - MinPitchValue) / WaveSize.Length;
        if (pitchValue <= MinPitchValue + range)
        {
            scale = WaveSize[0];
        }
        else if (pitchValue > MinPitchValue + range && pitchValue < MinPitchValue + 2 * range)
        {
            scale = WaveSize[1];
        }
        else
        {
            scale = WaveSize[2];
        }

        return scale;
    }
}
