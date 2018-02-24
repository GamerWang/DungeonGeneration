using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomComponent : MonoBehaviour {
    public GateComponent[] gates;

    float roadLength = 15;

    // private functions
    void CreateRoomSpaceCollider()
    {
        float spaceHeight = 0;
        float spaceWidth = 0;
        var spaceOffset = Vector2.zero;
        if(gates.Length == 4)
        {
            if(gates[0]!=null && gates[1] != null)
            {
                spaceHeight = gates[0].transform.localPosition.y - gates[1].transform.localPosition.y;
                spaceHeight = Mathf.Abs(spaceHeight);
                spaceHeight += roadLength;
                spaceOffset.y = (gates[0].transform.localPosition.y + gates[1].transform.localPosition.y) / 2;
            }
            if(gates[2]!=null && gates[3] != null)
            {
                spaceWidth = gates[2].transform.localPosition.x - gates[3].transform.localPosition.x;
                spaceWidth = Mathf.Abs(spaceWidth);
                spaceWidth += roadLength;
                spaceOffset.x = (gates[2].transform.localPosition.x + gates[3].transform.localPosition.x) / 2;
            }
        }
        if (spaceWidth != 0 && spaceHeight != 0)
        {
            var boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.size = new Vector2(spaceWidth, spaceHeight);
            boxCollider.offset = spaceOffset;
        }
    }

    // Use this for initialization
    private void Awake()
    {
        CreateRoomSpaceCollider();
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

    //    if(GUI.Button(new Rect(Screen.width - 200, 10, 200, 75), name, myButtonStyle))
    //    {
    //        foreach(var g in gates)
    //        {
    //            g.SetGateState();
    //        }
    //    }
    //}
}
