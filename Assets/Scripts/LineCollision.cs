﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineController), typeof(PolygonCollider2D))]
public class LineCollision : MonoBehaviour
{
    public UnityEvent OutsideLineEvent;
    bool outsideLine = false;

    LineController lineController;
    PolygonCollider2D polygonCollider;
    [SerializeField]
    List<GameObject> insideTrigger = new List<GameObject>();
    

    private void Awake()
    {
        lineController = GetComponent<LineController>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        if (OutsideLineEvent == null)
        {
            OutsideLineEvent = new UnityEvent();
        }
    }
    

    private void LateUpdate()
    {

        //Get all the positions from the line renderer
        Vector3[] positions = lineController.GetPositions();

        //If we have enough points to draw a line
        if (positions.Count() >= 2)
        {

            //Get the number of line between two points
            int numberOfLines = positions.Length - 1;

            //Make as many paths for each different line as we have lines
            polygonCollider.pathCount = numberOfLines;

            //Get Collider points between two consecutive points
            for (int i = 0; i < numberOfLines; i++)
            {
                //Get the two next points
                List<Vector2> currentPositions = new List<Vector2> {
                    positions[i],
                    positions[i+1]
                };

                List<Vector2> currentColliderPoints = CalculateColliderPoints(currentPositions);
                polygonCollider.SetPath(i, currentColliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
            }
        }
        else
        {
            polygonCollider.pathCount = 0;
        }

        if (outsideLine)
        {
            OutsideLineEvent.Invoke();   
        }

    }

    private List<Vector2> CalculateColliderPoints(List<Vector2> positions)
    {
        //Get The Width of the Line
        float width = lineController.GetWidth();

        // m = (y2 - y1) / (x2 - x1)
        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        //Calculate Vertex Offset from Line Point
        Vector2[] offsets = new Vector2[2];
        offsets[0] = new Vector2(-deltaX, deltaY);
        offsets[1] = new Vector2(deltaX, -deltaY);

        List<Vector2> colliderPoints = new List<Vector2> {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]
        };

        return colliderPoints;
    }

    public bool IsGameObjectInTrigger(GameObject obj)
    {
        return insideTrigger.IndexOf(obj) > -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (insideTrigger.IndexOf(collision.gameObject) <= -1)
        {
            insideTrigger.Add(collision.gameObject);
            Debug.Log("in");
            outsideLine = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        insideTrigger.Remove(collision.gameObject);
        Debug.Log("out");
        outsideLine = true;
    }
}
