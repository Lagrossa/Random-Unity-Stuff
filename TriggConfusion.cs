using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class TriggConfusion : MonoBehaviour
{
    //Defining Variables
    const float TAU = 6.28318530718f;
    //Control Variables
    //[Range(0, 300)]
    public int dotCount = 0;
    public float radius = .05f;
    public int density = 1;
    public Vector2 prevVec;
    public Vector2 currVec;
    public float thisRad = 1;
    public float circRad;

    public Color color2;

    public int upperBound = 32;
    public int lowerBound = 0;

    public int densityBoundUp = 12;
    public int densityBoundDown = 0; //DON'T GO NEGATIVE WITH DENSITY! ( I mean I guess you could but it would throw an error)

    public int densCount = 0;

    public int posMan = 0; //Positional Manager
    public int posEdit = 0;
    //Storage Variables
    public List<Vector2> points = new List<Vector2>();

    //Extraneous Variables
    public bool animating;
    public bool animatedens;
    public bool up = true;
    public bool down = false;

    public bool densUp = true;
    public bool densDown = false;


    //Frame
    public int skip = 0;
    //Frames to skip
    public int step = 10;

    void Start()
    {
        posEdit = 0;
        posMan = 0;
        points.Clear();

        drawPolygon();
    }

    void Update()
    {
        if (skip >= step)
        {
            if (animatedens)
            {
                if (densUp)
                {
                    density++;
                    if (density >= densityBoundUp)
                    {
                        densUp = false;
                        densDown = true;
                    }
                }
                else if (densDown)
                {
                    density--;
                    if (density <= densityBoundDown)
                    {
                        densUp = true;
                        densDown = false;
                    }
                }
            }

            if (animating)
            {
                if (up)
                {
                    dotCount++;
                    if (dotCount >= upperBound)
                    {
                        up = false;
                        down = true;
                    }
                }
                else if (down)
                {
                    dotCount--;
                    if (dotCount <= lowerBound)
                    {
                        up = true;
                        down = false;
                    }
                }
            }
            skip = 0;
        }
        else
        {
            skip++;
        }
    }

    public void drawPolygon()
    {
        points.Clear();
        for (int i = 0; i < dotCount; i++)
        {
            float t = (float)i / dotCount;

            float angRad = t * TAU;

            //sin and cos
            float x = Mathf.Cos(angRad);
            float y = Mathf.Sin(angRad);

            Vector2 cPoint = new Vector2(x, y);
            points.Add(cPoint);
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos(){

        for(int i = 0; i < points.Count + 1; i++){
            if(posEdit == 0){ //First pass
                prevVec = points[i];
                currVec = points[i + density];
                posMan = i + density;
                posEdit++;
            }
            else if(posMan <= points.Count) { // 2+ Pass
                prevVec = currVec;
                if(points.IndexOf(prevVec) + density <= points.Count - 1){ //If currentIndex + density <= Size-1
                    currVec = points[points.IndexOf(prevVec) + density];
                    posMan = points.IndexOf(prevVec) + density;
                }
                else{ //False, greater than

                    //CALCULATE ROLLOVER
                    posMan = points.IndexOf(prevVec) + density;
                    int rollOver = posMan % points.Count; //Literally the only time I've used modulo
                    currVec = points[rollOver]; // (example) 6-5 == 1
                    posMan = rollOver; // rollover position manager
                }
            }
            else if(posMan > points.Count){ // does the same as above


                //CALCULATE ROLLOVER
                posMan = points.IndexOf(prevVec) + density;
                int rollOver = posMan % points.Count; //Literally the only time I've used modulo
                currVec = points[rollOver]; // (example) 6-5 ==                 
                posMan = rollOver; // rollover position manager


                //OLD DRAFT -----------------
                //posMan = points.IndexOf(prevVec) + density;
                //int varStore = posMan;
                //currVec = points[posMan - points.Count]; // (example) 6-5 == 1
                //posMan = varStore - points.Count; // rollover position manager

            }

            Gizmos.DrawLine(prevVec, currVec);
            Handles.color = Color.red;
            Handles.DrawWireDisc(Vector3.zero, Vector3.forward, thisRad);
        }
    }
#endif

}
