using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ShapeData), false)] // False : chỉ áp dụng trực tiếp cho ShapeData và không áp dụng cho bất kỳ lớp con nào của ShapeData.
[CanEditMultipleObjects] //chỉnh sửa nhiều đối tượng cùng một lúc.
[System.Serializable] //để các đối tượng của lớp hoặc cấu trúc có thể được hiển thị và chỉnh sửa trong Inspector.
public class ShapeDataDrawer : Editor
{
   private ShapeData ShapeDataInstance => target as ShapeData;

    public override void OnInspectorGUI()
    {
       serializedObject.Update(); //nếu bạn thay đổi một giá trị trên Inspector của một đối tượng ShapeData thì đảm bảo rằng các thay đổi đó được lưu trữ.
        ClearBoardButton();
        EditorGUILayout.Space();

        DrawColumnsInputFields();
        EditorGUILayout.Space();

        if(ShapeDataInstance.board != null && ShapeDataInstance.columns > 0 && ShapeDataInstance.rows > 0)
        {
            DrawBoardTable();
        }

        //ApplyModifiedProperties(): Nó đảm bảo rằng bất kỳ thay đổi nào bạn thực hiện trên serializedObject thông qua custom editor,
        //sẽ được áp dụng và lưu trữ trong đối tượng thực tế.
        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(ShapeDataInstance);//các thay đổi được thực hiện trong custom editor sẽ được lưu lại và ghi nhận là đã thay đổi.
        }
    }

    private void ClearBoardButton()
    {
        if (GUILayout.Button("Clear Board"))
        {
            ShapeDataInstance.Clear();
        }
    }

    private void DrawColumnsInputFields()
    {
        var columsTemp  = ShapeDataInstance.columns;
        var rowTemp = ShapeDataInstance.rows;

        ShapeDataInstance.columns = EditorGUILayout.IntField("Columns", ShapeDataInstance.columns);
        ShapeDataInstance.rows = EditorGUILayout.IntField("Row", ShapeDataInstance.rows);

        if ((ShapeDataInstance.columns != columsTemp || ShapeDataInstance.rows != rowTemp) && ShapeDataInstance.columns > 0 && ShapeDataInstance.rows > 0)
        {
            ShapeDataInstance.CreateNewBoard();
        }
    }

    private void DrawBoardTable()
    {
        var tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10, 10, 10, 10);
        tableStyle.margin.left = 32;

        var headerColumStyle = new GUIStyle();
        headerColumStyle.fixedWidth = 65;
        headerColumStyle.alignment = TextAnchor.MiddleCenter;

        var rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 25;
        rowStyle.alignment = TextAnchor.MiddleCenter;

        var dataFieldStyle = new GUIStyle(EditorStyles.miniButtonMid);
        dataFieldStyle.normal.background = Texture2D.grayTexture;
        dataFieldStyle.onNormal.background = Texture2D.whiteTexture;

        for(var row = 0; row < ShapeDataInstance.rows; row++)
        {
           EditorGUILayout.BeginHorizontal(headerColumStyle);

            for (var column = 0; column < ShapeDataInstance.columns; column++)
            {
                EditorGUILayout.BeginHorizontal(rowStyle);
                var data = EditorGUILayout.Toggle(ShapeDataInstance.board[row].column[column], dataFieldStyle);
                ShapeDataInstance.board[row].column[column] = data;
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
