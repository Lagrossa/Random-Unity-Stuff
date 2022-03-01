using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MovementController : MonoBehaviour
{
    public Transform acPlane;
    public int rayDist = 1;
    public Transform plane;
    public Transform cammera;
    [Range(0,5)]
    public int scalar;
    public Vector3 position;
    public float positionMagnitude;
    public float radius;
    public float deLimiter = 2f;
    public int speedMultiplier = 2;
    public Vector3 pixel;
    public float pixelLerp;


    [Range(-20, 20)]
    public float xRotation;

    //Object Defs


    private void Start()
    {

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Ray rayz = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 planeVec = plane.position;
        Vector3 cameraVec = cammera.position;
        positionMagnitude = position.magnitude;
        Vector3 posNorm = position.normalized;
        radius = positionMagnitude;
        Vector3 distanceFromCamera = planeVec - cameraVec;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(cameraVec, radius);
        Gizmos.color = posNorm.magnitude * scalar >= radius ? Color.red : Color.green;
        Gizmos.DrawLine(cameraVec, position);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(cameraVec, rayz.direction + cameraVec);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(cameraVec, -rayz.direction + cameraVec);
        Gizmos.DrawSphere(position, .1f);
    }
#endif
    
    void Update()
    {
        Ray rayz = Camera.main.ScreenPointToRay(Input.mousePosition);
        position = rayz.GetPoint(rayDist);
        
        if (Input.GetKey(KeyCode.W))
        {
            cammera.Translate(rayz.direction * Time.deltaTime * speedMultiplier, Space.Self); 
        }
        else if (Input.GetKey(KeyCode.S))
        {
            cammera.Translate(-rayz.direction * Time.deltaTime * speedMultiplier, Space.Self);
        }

        pixel = Input.mousePosition;
        

        pixelLerp = Mathf.InverseLerp(-476, 1884, pixel.x);
        Debug.Log(pixelLerp);

        acPlane.rotation = Quaternion.Euler(Mathf.Lerp(-20, 20, pixelLerp),90,0);
        //cammera.rotation = Quaternion.Euler(Mathf.Lerp(-20, 20, pixelLerp), 0, 0);
        
        //Change Transform Rotate (Based on Degrees)
        
    }

    //Future Plans
    // Plane should move in direction of vector position.
    // "W" goes forward in Vector direction, "S" inverses the vector (go backwards)
    // "A" and "D" can rotate the plane (and the camera) via Unit Circle to make smooth transitions
    // Add Plane Model
}
