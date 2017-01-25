using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private GameManagerSettings gameManagerSettings;
    private MusicDecoder musicDecoder;
    private float waveIntervalTimeCount = 0f;
    private int Score = 0;

    public WaveGenerator waveGenerator;
    public Jump player;
    public float IdleGameOverInSeconds = 5f;
    private float period = 0;

    // Use this for initialization
    void Start () {
        gameManagerSettings = this.GetComponent<GameManagerSettings>();
        musicDecoder = this.GetComponent<MusicDecoder>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<GameManagerSettings>().GameOver)
        {

            if (CanCreateWave())
            {
                CreateWave();
            }
        }
        else
        {
            if(period >= IdleGameOverInSeconds) {
             GetComponent<DataController>().SubmitNewPlayerScore(Score);
             SceneManager.LoadScene("MenuInicial");
            }
            else
            {
                period += Time.fixedDeltaTime;
            }

        }
    }

    private bool CanCreateWave()
    {
        waveIntervalTimeCount += Time.deltaTime;
        return waveIntervalTimeCount > gameManagerSettings.WaveInterval &&  
            (player.IsNearToGround(gameManagerSettings.DistanceToGroundToNextWave) || player.IsGrounded());
    }

    private void CreateWave()
    {
            waveIntervalTimeCount = 0;
        var pitchValue = musicDecoder.GetPitchValue();
        if (pitchValue == 0)
        {
            waveGenerator.CreateWave();
        }
        else
        {
            waveGenerator.CreateWave(pitchValue);
        }
    }

    public void AddScore()
    {
        Score += 1;
    }

    public int GetScore()
    {
        return Score;
    }

}

