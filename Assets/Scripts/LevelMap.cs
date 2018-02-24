using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMap : MonoBehaviour {
    public int[] size;
    public int[] startPos;
    public CustomArray[] map;
    public int[,] map2;
}

[System.Serializable]
public class CustomArray
{
    public int[] Array;

    public int this[int index]
    {
        get {
            return Array[index];
        }
        set
        {
            Array[index] = value;
        }
    }
        
    public CustomArray()
    {
        this.Array = new int[0];
    }

    public CustomArray(int index)
    {
        this.Array = new int[index];
    }
}
