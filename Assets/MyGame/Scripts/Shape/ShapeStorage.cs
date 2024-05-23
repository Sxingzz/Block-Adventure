using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    public List<ShapeData> shapeData;
    public List<Shape> shapelist;

    private void OnEnable()
    {
        GameEvent.RequesNewShapes += RequesNewShapes;
    }


    private void OnDisable()
    {
        GameEvent.RequesNewShapes -= RequesNewShapes;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var shape in shapelist)
        {
            var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
            shape.CreateShape(shapeData[shapeIndex]);
        }
    }

    public Shape GetCurrentSelectedShape()
    {
        foreach (var shape in shapelist)
        {
            if (shape.IsOnStartPosition() == false && shape.IsAnyOfShapeSquareActive()) return shape;
        }
        Debug.LogError("There is no shape selected");
        return null;
    }

    private void RequesNewShapes()
    {
        foreach (var shape in shapelist)
        {
            var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
            shape.RequestNewShape(shapeData[shapeIndex]);
        }
    }



}
