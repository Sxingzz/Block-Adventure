using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] //[CreateAssetMenu(fileName = "NewData", menuName = "Custom Data")]
[System.Serializable] //có thể lưu trữ và khôi phục lại trạng thái của đối tượng.
public class ShapeData : ScriptableObject
{
    [System.Serializable]
    public class Row
    {
        public bool[] column;
        private int _size = 0;

        public Row() { } //default constructor
        //khi sử dụng mảng Row[] board trong lớp ShapeData,
        //Unity yêu cầu mỗi phần tử trong mảng phải có một default constructor.
        //Điều này là do Unity sử dụng tuần tự hóa (serialization) để lưu trữ và tải lại dữ liệu,
        //và quá trình tuần tự hóa cần có khả năng tạo ra các đối tượng mà không cần biết trước các tham số cần thiết.

        public Row(int size)
        {
            CreateRow(size);
        }
        public void CreateRow(int size)
        {
            _size = size;
            column = new bool[_size];
            ClearRow();
        }
        public void ClearRow()
        {
            for (int i = 0; i < _size;i++)
            {
                column[i] = false;
            }
        }
        
    }

    public int columns = 0;
    public int rows = 0;
    public Row[] board; // yêu cầu default constructor

    public void Clear()
    {
        for(var i = 0; i < rows; i++)
        {
            board[i].ClearRow();
        }
    }

    public void CreateNewBoard()
    {
        board = new Row[rows];

        for (var i = 0; i<rows; i++)
        {
            board[i] = new Row(columns);
        }
    }
}
