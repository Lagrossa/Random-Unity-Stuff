using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class BezManager : MonoBehaviour
{
    public Transform a;
    public Transform b;
    public Transform c;

    [Range(0, 1)]
    public float lerp;

    public float lerpSpeed = .005f;
    public float radius = .05f;

    public bool lerpAnimate;
    public bool lerpPositive;
    public bool lerpNegative;
   
    public Vector2 currentPosition;
    public Vector2 previousPosition;

    private void Update()
    {
        if (lerp >= 1)
        {
            lerpNegative = false;
            lerpPositive = true;
        }
        else if (lerp <= 0)
        {
            lerpPositive = false;
            lerpNegative = true;
        }

        if (lerpPositive)
        {
            lerp -= lerpSpeed;
        }
        else if (lerpNegative)
        {
            lerp += lerpSpeed;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 aVect = a.position;
        Vector2 bVect = b.position;
        Vector2 cVect = c.position;
        Vector2 bToALerp = Vector2.Lerp(bVect, aVect, lerp);
        Vector2 aToCLerp = Vector2.Lerp(aVect, cVect, lerp);
        Vector2 bezierPosition = Vector2.Lerp(bToALerp, aToCLerp, lerp);
        Gizmos.DrawLine(bVect, aVect);
        Gizmos.DrawLine(aVect, cVect);
        Gizmos.DrawSphere(bToALerp, .05f);
        Gizmos.DrawSphere(aToCLerp, .05f);
        Gizmos.DrawLine(bToALerp, aToCLerp);
        Gizmos.color = Color.red;   
        Gizmos.DrawSphere(Vector2.Lerp(bToALerp, aToCLerp, lerp), .05f);
        Gizmos.DrawLine(bVect, Vector2.Lerp(bToALerp, aToCLerp, lerp)); 
        Gizmos.DrawLine(cVect, Vector2.Lerp(bToALerp, aToCLerp, lerp));
        currentPosition = new Vector2 (bezierPosition.x, bezierPosition.y);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(currentPosition, previousPosition);
        previousPosition = currentPosition;
    }
#endif


    private float Distance(Vector2 a, Vector2 b)
    {
        // Sqrt((X1-X2)^2 + (Y1-Y2)^2))
        return (Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2)));
    }
}
