using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInputModeByButton : MonoBehaviour {
    public InputModeByButton.InputType t;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setMode()
    {
        InputModeByButton.inputType = t;

        Debug.Log(InputModeByButton.inputType);
    }
}
