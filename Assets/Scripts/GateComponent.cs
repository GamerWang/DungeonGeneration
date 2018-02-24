using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateComponent : MonoBehaviour {
    public GameObject gatePrefab;
    GameObject gateObject;

    public bool open = false;

    public void ChangeGateState()
    {
        if (open)
            CloseGate();
        else
            OpenGate();
    }

    public void SetGateState(bool state)
    {
        if (state)
            OpenGate();
        else
            CloseGate();
    }

    public void OpenGate()
    {
        open = true;
        gateObject.SetActive(true);
    }

    public void CloseGate()
    {
        open = false;
        gateObject.SetActive(false);
    }

    public void InstantiateGate()
    {
        gateObject = Instantiate(gatePrefab, transform);
        gateObject.SetActive(open);
    }

    // Use this for initialization
    private void Awake()
    {
        InstantiateGate();
    }

    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void OnGUI()
    //{
    //    GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        
    //    myButtonStyle.fontSize = 30;

    //    if (GUI.Button(new Rect(Screen.width - 200, (transform.position.x/3 - transform.position.y)*30 + 200, 200, 75), name, myButtonStyle))
    //    {
    //        if (open)
    //        {
    //            CloseGate();
    //        }
    //        else
    //        {
    //            OpenGate();
    //        }
    //    }
    //}
}
