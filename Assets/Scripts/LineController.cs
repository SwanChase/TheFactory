using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineController))]
public class LineController : MonoBehaviour
{
    public List<Transform> nodes;
    private LineRenderer lr;

    public List<Transform> NodesList
    {
        get
        {
            return nodes;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = nodes.Count;

    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPositions(nodes.ConvertAll(n => n.position - new Vector3(0, 0, 0.5f)).ToArray());   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (nodes != null) nodes.ForEach(p => Gizmos.DrawSphere(p.position, 0.1f));
    }

    public Vector3[] GetPositions() {
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        return positions;
    }

    public float GetWidth() {
        return lr.startWidth;
    }
}
