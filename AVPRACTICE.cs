using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class AVPRACTICE : MonoBehaviour
{
    public float angle;
    public Transform betterMouse;
    public float normalizer = 1;
    public Transform startX;
    public float radius = 1f;

    //Animation variables
    public float stabilizer = .01f;
    [Range(0,360)]
    public float angDeg;
    public Vector3 acAngle;
    public bool animating;
    public bool angUp;

    void Update(){

        if (animating) { //wait this is so good I should save it for later
            if(angUp && angDeg < 360) {
                angDeg += stabilizer;
            }
            if(angDeg >= 360 || angDeg <= 0) { 
                angUp = angDeg switch
                {
                    >=360 => false,
                    <=0 => true,
                    _ => false,
                };
            }
            if(!angUp && angDeg > 0) {
                angDeg -= stabilizer;
            }

            acAngle = new Vector3(Mathf.Cos(angDeg), Mathf.Sin(angDeg), 0);
        }
    }

    void Start() {

    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        Vector3 newMouse = betterMouse.position;
        //Gizmos.DrawLine(Vector3.zero, newMouse);
        angle = Mathf.Rad2Deg * Mathf.Atan2(newMouse.y, newMouse.x);
        //Debug.Log(angle); //Angle of newMouse
        Handles.color = Color.green;
        Handles.DrawLine(Vector3.zero, acAngle);
        Handles.color = Color.red;
        Handles.DrawWireArc(Vector3.zero, Vector3.forward, startX.position, Mathf.Rad2Deg * angDeg, radius);
        Handles.color = Color.cyan;
        //Draw the opposite angle for the [Missing] Portion
        Handles.DrawWireArc(Vector3.zero, Vector3.forward, startX.position, Mathf.Pow(Mathf.Rad2Deg * angDeg, -1), radius);
    }
#endif
}
