using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSkripta : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void WeirdMode()
    {
        Application.LoadLevel("WeirdMode");
    }

    public void NormalMode()
    {
        Application.LoadLevel("NormalMode");
    }
}
