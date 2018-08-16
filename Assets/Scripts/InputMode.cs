using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMode : MonoBehaviour {

    public enum InputType
    {
        MOVE,
        ROT,
        SCALE,
        NONE
    }

    public InputType inputType = InputType.NONE;
    public GameObject ScaleTop;
    public GameObject RotTop;
    public GameObject MoveTop;

    public static InputMode instance = null;

    void Awake()
    {
        //AudioManagerインスタンスが存在したら
        if (instance != null)
        {
            //今回インスタンス化したAudioManagerを破棄
            Destroy(this.gameObject);
            //AudioManagerインスタンスがなかったら
        }
        else if (instance == null)
        {
            //このAudioManagerをインスタンスとする
            instance = this;
        }
        //シーンを跨いでもAudioManagerインスタンスを破棄しない
        DontDestroyOnLoad(this.gameObject);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setInputMode(InputType t)
    {
        Debug.Log(t);

        inputType = t;

        switch(inputType)
        {
            case InputType.MOVE:
                ScaleTop.SetActive(false);
                RotTop.SetActive(false);
                break;
            case InputType.ROT:
                ScaleTop.SetActive(false);
                MoveTop.GetComponent<BoxCollider>().enabled = false;
                break;
            case InputType.SCALE:
                RotTop.SetActive(false);
                MoveTop.GetComponent<BoxCollider>().enabled = false;
                break;
            case InputType.NONE:
                ScaleTop.SetActive(true);
                RotTop.SetActive(true);
                MoveTop.GetComponent<BoxCollider>().enabled = true;
                break;
        }
    }

    public void resetInputType(InputType t)
    {
        ScaleTop.SetActive(true);
        RotTop.SetActive(true);
        MoveTop.GetComponent<BoxCollider>().enabled = true;
    }
}
