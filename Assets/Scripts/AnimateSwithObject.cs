using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSwithObject : MonoBehaviour {
    public float bpm = 60.0f;
    public GameObject[] hart_array;
    float switchTime;
    float time = 0;
    int index = 0;

    // Use this for initialization
    void Start()
    {
        //for (int i = 1; i < hart_array.Length; i++)
        //{
        //    hart_array[i].SetActive(false);
        //}
        switchTime = (60.0f / bpm) / (float)hart_array.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //if (checkMeshObject.activeSelf)
        //{
        //    if (!checkMeshObject.GetComponent<MeshRenderer>().enabled)
        //    {
        //        return;
        //    }
        //}
        time += Time.deltaTime;
        if (time > switchTime)
        {
            Debug.Log(index);
            hart_array[index].SetActive(false);
            index++;
            if (index >= hart_array.Length)
            {
                index = 0;
            }
            hart_array[index].SetActive(true);
            time = 0.0f;
        }
        //if (checkMeshObject.activeSelf)
        //{
        //    checkMeshObject.SetActive(false);
        //    gameObject.GetComponent<AudioSource>().enabled = true;
        //}

    }
}
