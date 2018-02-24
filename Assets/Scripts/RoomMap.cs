using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMap : MonoBehaviour {
    public int[] size = new int[2] { 5, 5 };
    public int[] startPos = new int[2] { 1, 3 };
    public int[,] roomMap = new int[7, 7] {
        {0,0,0,0,0,0,0 },
        {0,0,0,1,0,0,0 },
        {0,0,0,2,0,0,0 },
        {0,1,2,1,2,1,0 },
        {0,0,0,2,0,2,0 },
        {0,0,0,1,2,1,0 },
        {0,0,0,0,0,0,0 }
    };
}
