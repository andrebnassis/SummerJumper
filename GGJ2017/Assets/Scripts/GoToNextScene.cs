using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour {

    // Use this for initialization
    public string sceneName;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Jump") || Input.touchCount > 0)
        {
            SceneManager.LoadScene(sceneName);
        }
	}
}
