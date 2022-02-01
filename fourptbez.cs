using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
public class fourptbez : MonoBehaviour
{
    public Transform ptA;
    public Transform ptB;
    public Transform ptC;
    public Transform ptD;

    [Range(0,1)]
    public float lerp;
    public float radii = .04f;
    private void OnDrawGizmos()
    {
        //Positions
        Vector2 a = ptA.position;
        Vector2 b = ptB.position;
        Vector2 c = ptC.position;
        Vector2 d = ptD.position;

        //Lerps
        Vector2 aBLerp = (1.0f - lerp) * a + b * lerp;
        Vector2 bCLerp = (1.0f - lerp) * b + c * lerp;
        Vector2 cDLerp = (1.0f - lerp) * c + d * lerp;

        //Draw the stuff~! FIRST ITERATION
        Gizmos.DrawLine(a,b);
        Gizmos.DrawLine(b, c);
        Gizmos.DrawLine(c, d);
        Gizmos.DrawSphere(aBLerp, radii);
        Gizmos.DrawSphere(bCLerp, radii);
        Gizmos.DrawSphere(cDLerp, radii);

        //SECOND ITERATION
        Gizmos.DrawLine(aBLerp, bCLerp);
        Gizmos.DrawLine(bCLerp, cDLerp);
        Vector2 aBBCLerp = (1.0f - lerp) * aBLerp + bCLerp * lerp;
        Vector2 bCCDLerp = (1.0f - lerp) * bCLerp + cDLerp * lerp;
        Gizmos.DrawLine(aBBCLerp, bCCDLerp);
        Gizmos.DrawSphere(aBBCLerp, radii);
        Gizmos.DrawSphere(bCCDLerp, radii);

        //THIRD ITERATION
        Vector2 lerpApex = (1.0f - lerp) * aBBCLerp + bCCDLerp * lerp;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(lerpApex, radii);

        //FINALITIES
        Handles.DrawBezier(a, d, b, c, Color.red, Texture2D.blackTexture, .3f);
    }
#endif
}
