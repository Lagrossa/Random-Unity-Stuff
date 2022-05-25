//DEVELOPED IN UNITY

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class trigPractice : MonoBehaviour
{
    //Variables
    public int dots = 16;
    const float TAU = 6.28318530718f;
    public float radius = .01f;
    public Vector3 prevVec;
    public Vector3 currVec;
    public List<Vector3> points;
    public float radi2;
    //[Range(0,1)]
    public float lerp; // use lerp modulo 1 to normalize it to 0-1 as opposed to range which restricts the animation
    public int step = 0;
    public int deNormalizer = 10;
    public float offsetStart = 0;
    public float finalOffset;
    public float offset = .05f;
    public bool animating;
    public bool set;
    public bool updating;
    public float pastTime;
    public int density = 1;
    public Vector3 lastPoint;
    public Vector3 lerpVec = new Vector3(1, 0, 0);
    public float screenSpaceTest = 1f;

    //More Animation Variables
    public bool altAnim;
    public bool upLerp;
    public bool downLerp;
    //Methods

    private void Start()
    {
        //generatePolygon();
        pastTime = Time.time;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos(){
        generatePolygon();
        circConnection();
    }
#endif
    private void Update()
    {
        //All of the animations

        if (altAnim){
            if(step >= deNormalizer){
                    if (lerp < .98 && upLerp){
                        lerp += .01f;
                    }
                    else if (lerp >= .98){
                        upLerp = false;
                        downLerp = true;
                        lerp -= .01f;
                    }
                    else if (lerp <= 0 && downLerp){
                        upLerp = true;
                        downLerp = false;
                        lerp += .01f;
                    }
                    else if (lerp > 0 && downLerp){
                        lerp -= .01f;
                    }
                    step = 0;
                }
            step++;
        }


        if (set) 
        {
            dots = 72;
            offset = .048f;
        }
        if (animating) {
            if (updating)
            {
               if(Time.time - pastTime > 3f){
                    dots++;
                    pastTime = Time.time;
                }
            }
            if(step >= deNormalizer){
                lerp += .01f;
                step = 0;
            }
            else {
                step++;
            }
        }
    }

    void generatePolygon() //Creates the polygon with basic trigonometry
    {
        points.Clear();

        for (int i = 0; i < dots; i++)
        {
            //my interpolate-r. gives me the proportion of the circle in context.
            //For example, if we're on point 1 out of 4 total points, the interpolate-r will be .25 (1/4)
            float t = (float)i / dots;

            float angRad = t * TAU; //Normalizes it to TAU (which is less confusing than PI)
            /*

                    * 1/4 TAU (1/2 PI)




            * .5 TAU         * 0 TAU/1 TAU



                    * 3/4 TAU

            */
            //Cosine is used to find the x value of an angle.
            float x = Mathf.Cos(angRad);
            //Sine can be used to find the y value of the angle.
            float y = Mathf.Sin(angRad);

            //vector stuff
            //Creates a vector based off of the x and y coordinates of the current point
            Vector3 point = new Vector2(x, y);
            points.Add(point);
            Handles.DrawLine(prevVec, point); //Draws the polygon
            prevVec = point;
            Handles.DrawSolidDisc(point, Vector3.forward, radius);
        }
    }
    void circConnection()
    {
        finalOffset = 0;
        //lastPoint = Vector2.negativeInfinity; //(test code for alternate patterns)
        for(int i = 0; i < points.Count; i++){
            float t = (float)i / points.Count;
            Handles.color = Color.white;
            //Gizmos.DrawLine(points[i], points[(i + (points.Count / 2)) % points.Count]); //(test code for alternate patterns)
            Handles.color = Color.cyan;
            finalOffset %= offsetStart;
            Vector3 thisPoint = Lerp(points[i], points[(i + (points.Count / 2)) % points.Count], ((lerp % 1) + finalOffset) % 1);
            Vector3 eOThisPoint = Lerp(points[i], points[(i + (points.Count / 2)) % points.Count], 1);
                //Inverse lerp "This Point" to 100%  
            Handles.DrawDottedLine(thisPoint, eOThisPoint, screenSpaceTest);
            Handles.DrawSolidDisc(thisPoint, Vector3.forward ,radi2);
            //Gizmos.DrawLine(lastPoint, thisPoint); //(test code for alternate patterns)
            lastPoint = thisPoint;
            finalOffset += offset; 
        }
    }

    public Vector3 Lerp(Vector3 a, Vector3 b, float interpolater){ //Linear interpolation mathematical formula
        return (1.0f - interpolater) * a + b * interpolater;
    }

}
