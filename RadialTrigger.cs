using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class RadialTrigger : MonoBehaviour
{
    public Transform circ;
    public Transform pointB;
    [Range(0, 4)]
    public float radius;
    public bool inRadius;
    public Vector2 direction;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 b = pointB.position;
        Vector2 circVec = circ.position;
        //By using distance*distance and radius*radius, program is optimized (doesn't require the use of Sqrt())
        float distSq = Mathf.Pow((circVec.x - b.x), 2) + Mathf.Pow(circVec.y - b.y, 2);
        if (distSq > radius * radius)
        {
            Handles.color = Color.red;
            inRadius = false;
        }
        else
        {
            Handles.color = Color.green;
            inRadius = true;
        }
        Handles.DrawWireDisc(circ.position, new Vector3(0, 0, 1), radius);
        direction = b - circVec;
        Vector2 radiusPoint;
        //Vector2 circPlusDir = new Vector2(circVec.x + radius, circVec.y);
        //Gizmos.DrawLine(new Vector2(circVec.x + radius, circVec.y + radius). , circVec+ direction);
        // (Doesn't work because at 0,0 - The line is drawn from 1,1 as opposed to the circle's radius.
    }
#endif
}
