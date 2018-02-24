using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LevelMap))]
public class LevelMapEditor : Editor {
    int rowCount;
    int columnCount;

    public override void OnInspectorGUI()
    {
        LevelMap myLevelMap = (LevelMap)target;

        var mapZoneStyle = new GUIStyle(GUI.skin.box);
        mapZoneStyle.alignment = TextAnchor.MiddleCenter;

        var mapLabelStyle = new GUIStyle(GUI.skin.label);
        mapLabelStyle.alignment = TextAnchor.MiddleCenter;

        var mapGridStyle = new GUIStyle(GUI.skin.textField);
        mapGridStyle.alignment = TextAnchor.MiddleCenter;
        var textGridLayoutOptions = new GUILayoutOption[] {
            GUILayout.MaxWidth(20),
        };

        var yxFieldStyle = new GUIStyle(GUI.skin.textField);
        yxFieldStyle.margin = new RectOffset() {
            top = 3,
            bottom = 3,
            left = 0,
            right = 30,
        };
        var yxFieldOptions = new GUILayoutOption[]
        {
            GUILayout.ExpandWidth(true)
        };
        myLevelMap.size[0] = EditorGUILayout.IntSlider("Total Rows", myLevelMap.size[0], 1, 10);
        myLevelMap.size[1] = EditorGUILayout.IntSlider("Total Columns", myLevelMap.size[1], 1, 10);
        myLevelMap.startPos[0] = EditorGUILayout.IntField("Row Number", myLevelMap.startPos[0], yxFieldStyle, yxFieldOptions);
        myLevelMap.startPos[1] = EditorGUILayout.IntField("Column Number", myLevelMap.startPos[1], yxFieldStyle, yxFieldOptions);

        var rowCount = myLevelMap.size[0];
        var columnCount = myLevelMap.size[1];

        EditorGUILayout.BeginVertical(mapZoneStyle);
        if(rowCount > 0 && columnCount > 0)
        {
            EditorGUILayout.LabelField("Map Field", mapLabelStyle);
            if(myLevelMap.map == null)
            {
                myLevelMap.map = new CustomArray[rowCount + 2];
                foreach(var c in myLevelMap.map)
                {
                    c.Array = new int[columnCount + 2];
                }
            }
            else if (myLevelMap.map.Length != rowCount + 2 || myLevelMap.map[0].Array.Length != columnCount + 2)
            {
                var oldMap = myLevelMap.map;
                myLevelMap.map = new CustomArray[rowCount + 2];
                for(var i = 0; i < rowCount + 2; i++)
                {
                    myLevelMap.map[i] = new CustomArray(columnCount + 2);
                }
                var minRow = rowCount + 2 > oldMap.Length ? oldMap.Length - 2 : rowCount;
                var minColumn = columnCount + 2 > oldMap[0].Array.Length ? oldMap[0].Array.Length - 2 : columnCount;

                for (var i = 1; i <= minRow; i++)
                {
                    for (var j = 1; j <= minColumn; j++)
                    {
                        myLevelMap.map[i][j] = oldMap[i][j];
                    }
                }
            }

            EditorGUILayout.BeginHorizontal();
            for(var i = 0; i < columnCount + 1; i++)
            {
                EditorGUILayout.LabelField(i.ToString(), mapLabelStyle, textGridLayoutOptions);
            }
            EditorGUILayout.EndHorizontal();

            for(var i = 0; i < rowCount; i++){
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField((i + 1).ToString(), mapLabelStyle, textGridLayoutOptions);
                for(var j = 0; j < columnCount; j++)
                {
                    myLevelMap.map[i + 1][j + 1] = EditorGUILayout.IntField(myLevelMap.map[i + 1][j + 1], mapGridStyle, textGridLayoutOptions);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.LabelField("No Valid Grid");
        }
        EditorGUILayout.EndVertical();
    }
}
