using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuByButton : MonoBehaviour {
    public string menuScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}
