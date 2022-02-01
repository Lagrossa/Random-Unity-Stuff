using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SpaceTrans : MonoBehaviour
{
    //Variables


    public Transform localpoint;
    public Transform point;
    public Vector2 worldTransVectorToPoint;
    public bool grabPoint;
    public bool worldTransform;
    public bool localTransform;
    [Range(0, 1)]
    public float worldDotProduct;
    [Range(0, 1)]
    public float localDotProduct;
    public float radius;
    public float angleInWorld;
    public Vector2 vectorViewer;
     
    // 
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 pointvec = point.position;
        Vector2 localvec = localpoint.position;
        float magnitude = Mathf.Sqrt(Mathf.Pow(worldTransVectorToPoint.x - 0, 2) + Mathf.Pow(worldTransVectorToPoint.y - 0, 2));
        worldTransVectorToPoint = grabPoint ? pointvec : worldTransVectorToPoint;
        worldTransVectorToPoint = worldTransVectorToPoint.normalized;
        if (worldTransform)
        {
            // Cool effects later ...... Gizmos.DrawLine(Vector2.zero, Vector2.Lerp(worldTransVectorToPoint,));
            Gizmos.DrawLine(Vector2.zero, worldTransVectorToPoint);
            radius = magnitude;
            Handles.color = Color.cyan;
            Handles.DrawWireDisc(Vector2.zero, new Vector3(0, 0, 1), radius);
            // FINISH LATER worldDotProduct = (worldTransVectorToPoint.x*1 ) + (worldTransVectorToPoint.y * worldTransVectorToPoint.y);
            worldDotProduct = Vector2.Dot(worldTransVectorToPoint, new Vector2 (1,0));
            Vector2 newVect = worldToLocal(worldTransVectorToPoint, localpoint, worldDotProduct);
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(localvec, localvec + worldTransVectorToPoint);
            localDotProduct = Vector2.Dot(new Vector2(localvec.x, 0), newVect);
        }


        //Vector2 vect1 = new Vector2(2, 2);
        //Vector2 vect2 = new Vector2(0, 2);
        //Vector2 vect3 = vect1 + vect2;
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(Vector2.zero, vect3);
        //vectorViewer = vect3;


    }
#endif
    private Vector2 worldToLocal(Vector2 vector, Transform locally, float dotProduct)
    {
        Vector2 vecty;
        Vector2 localorigin = locally.position;
        //Add vector to position of locally
        //vecty = vector + localorigin;
        angleInWorld = (Vector3.Angle(Vector3.zero, localorigin));
        vecty = transform.TransformPoint(new Vector2(localorigin.x + vector.x, localorigin.y + vector.y));

        return vecty;
    }

  
}
