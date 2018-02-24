using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public LevelMap currentMap;
    public GameObject roadPrefab;
    public List<GameObject> roomPrefabs;

    public float basicDistance = 10;

    static System.Random random = new System.Random();

    // private references & variables
    List<int[]> roomPointList = new List<int[]>();
    GameObject[,] roomObjects;

    void InitializeLevelMap()
    {
        if (roomObjects != null)
        {
            if (roomPointList.Count != 0)
            {
                foreach(var r in roomPointList)
                {
                    Destroy(roomObjects[r[0], r[1]]);
                    roomObjects[r[0], r[1]] = null;
                }
            }
        }
        roomPointList = new List<int[]>();
        
        var size = currentMap.size;
        roomObjects = new GameObject[size[0] + 2, size[1] + 2];

        var roomMap = currentMap.map;
        var startPos = currentMap.startPos;
        var recordMap = new int[size[0] + 2, size[1] + 2];
        Stack<int[]> roomStack = new Stack<int[]>();
        roomStack.Push(startPos);
        recordMap[startPos[0], startPos[1]] = 1;
        while(roomStack.Count != 0)
        {
            var currentPos = roomStack.Pop();
            roomPointList.Add(currentPos);
            // find top
            if (roomMap[currentPos[0] - 1][currentPos[1]] == 2)
            {
                if (recordMap[currentPos[0] - 2, currentPos[1]] == 0)
                {
                    roomStack.Push(new int[] { currentPos[0] - 2, currentPos[1] });
                    recordMap[currentPos[0] - 2, currentPos[1]] = 1;
                }
            }
            // find bottom
            if (roomMap[currentPos[0] + 1][currentPos[1]] == 2)
            {
                if (recordMap[currentPos[0] + 2, currentPos[1]] == 0)
                {
                    roomStack.Push(new int[] { currentPos[0] + 2, currentPos[1] });
                    recordMap[currentPos[0] + 2, currentPos[1]] = 1;
                }
            }
            // find left
            if (roomMap[currentPos[0]][currentPos[1] - 1] == 2)
            {
                if (recordMap[currentPos[0], currentPos[1] - 2] == 0)
                {
                    roomStack.Push(new int[] { currentPos[0], currentPos[1] - 2 });
                    recordMap[currentPos[0], currentPos[1] - 2] = 1;
                }
            }
            // find right
            if (roomMap[currentPos[0]][currentPos[1] + 1] == 2)
            {
                if (recordMap[currentPos[0], currentPos[1] + 2] == 0)
                {
                    roomStack.Push(new int[] { currentPos[0], currentPos[1] + 2 });
                    recordMap[currentPos[0], currentPos[1] + 2] = 1;
                }
            }
        }
        roomPointList.Sort((r1, r2) => {
            if (r1[0] != r2[0])
            {
                return r1[0].CompareTo(r2[0]);
            }
            else
            {
                return r1[1].CompareTo(r2[1]);
            }
        });
        foreach(var r in roomPointList)
        {
            var roomPrefabCount = roomPrefabs.Count;
            var roomPrefabID = random.Next(roomPrefabCount);

            var currentPrefab = roomPrefabs[roomPrefabID];
            
            var currentRoom = Instantiate(currentPrefab, new Vector3(-1000, -1000, 0), Quaternion.Euler(Vector3.zero));
            var roomComponent = currentRoom.GetComponent<RoomComponent>();
            
            var localTopGatePos = roomComponent.gates[0].transform.localPosition;
            var localLeftGatePos = roomComponent.gates[2].transform.localPosition;

            var roomPosition = new Vector3(r[1] * basicDistance, -r[0] * basicDistance, 0);

            var topRoomBottomGatePos = new Vector3(-1000, -1000, 0);
            var leftRoomRightGatePos = new Vector3(-1000, -1000, 0);

            // align top
            if (roomMap[r[0] - 1][r[1]] == 2)
            {
                if (roomObjects[r[0] - 2, r[1]] != null)
                {
                    var topRoom = roomObjects[r[0] - 2, r[1]];
                    var topRoomComponent = topRoom.GetComponent<RoomComponent>();
                    topRoomBottomGatePos = topRoomComponent.gates[1].transform.position;
                    var currentXPosition = topRoomBottomGatePos.x - localTopGatePos.x;
                    roomPosition.x = currentXPosition;

                    topRoomComponent.gates[1].GetComponent<GateComponent>().OpenGate();
                    roomComponent.gates[0].GetComponent<GateComponent>().OpenGate();
                }
            }
            // align left
            if (roomMap[r[0]][r[1] - 1] == 2)
            {
                if(roomObjects[r[0],r[1]-2] != null)
                {
                    var leftRoom = roomObjects[r[0], r[1] - 2];
                    var leftRoomComponent = leftRoom.GetComponent<RoomComponent>();
                    leftRoomRightGatePos = leftRoomComponent.gates[3].transform.position;
                    var currentYPosition = leftRoomRightGatePos.y - localLeftGatePos.y;
                    roomPosition.y = currentYPosition;

                    leftRoomComponent.gates[3].GetComponent<GateComponent>().OpenGate();
                    roomComponent.gates[2].GetComponent<GateComponent>().OpenGate();
                }
            }

            currentRoom.transform.position = roomPosition;

            if(topRoomBottomGatePos.x != -1000)
            {
                var currentTopGatePos = roomComponent.gates[0].transform.position;
                var roadPosition = Vector3.Lerp(topRoomBottomGatePos, currentTopGatePos, 0.5f);
                var roadLength = topRoomBottomGatePos.y - currentTopGatePos.y;
                var topRoadObject = Instantiate(roadPrefab, roadPosition, Quaternion.Euler(Vector3.zero));
                topRoadObject.GetComponent<SpriteRenderer>().size = new Vector2(1, roadLength);
            }
            if(leftRoomRightGatePos.x != -1000)
            {
                var currentLeftGatePos = roomComponent.gates[2].transform.position;
                var roadPosition = Vector3.Lerp(leftRoomRightGatePos, currentLeftGatePos, 0.5f);
                var roadLength = currentLeftGatePos.x - leftRoomRightGatePos.x;
                var leftRoadObject = Instantiate(roadPrefab, roadPosition, Quaternion.Euler(Vector3.zero));
                leftRoadObject.GetComponent<SpriteRenderer>().size = new Vector2(roadLength, 1);
            }

            roomObjects[r[0], r[1]] = currentRoom;
        }
    }

    // Use this for initialization
    void Start () {
        InitializeLevelMap();
	}
	
	// Update is called once per frame
	void Update () {
    }
}
