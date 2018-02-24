using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThinksquirrelSoftware.Utilities;

public class Shaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if(GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 200, 100), "Shake"))
        {
            CameraShake.Shake();
        }
    }
}
