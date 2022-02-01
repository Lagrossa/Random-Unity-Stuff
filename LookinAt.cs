using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookinAt : MonoBehaviour
{
    public bool lookingAt;
    [Range(0, 1)]
    public float threshold;
    public float dotProduct;
    public Transform player;
    public Transform Trigger;
    public Transform zero;
    private void OnDrawGizmos()
    {
        Vector2 zeroVec = zero.position;
        Vector2 trig = Trigger.position;
        Vector2 play = player.position;
        Vector2 trigRelative = Trigger.position - zero.position;
        Vector2 playRelative = player.position - zero.position;
        //Vector2 plusXPar = new Vector2(Trigger.position.x + 1, Trigger.position.y);
        //Vector2 minusXPar = new Vector2(Trigger.position.x - 1, Trigger.position.y);
        //Vector2 plusYPar = new Vector2(Trigger.position.x, Trigger.position.y + 1);
        //Vector2 minusYPar = new Vector2(Trigger.position.x, Trigger.position.y - 1);
        //Gizmos.DrawLine(plusXPar, minusXPar);
        //Gizmos.DrawLine(plusYPar, minusYPar);
        Gizmos.DrawLine(zeroVec, new Vector2(zeroVec.x + 1, 0 + zeroVec.y));
        Gizmos.DrawLine(zeroVec, new Vector2(0 + zeroVec.x, zeroVec.y + 1));
        Gizmos.DrawLine(zeroVec, trigRelative + zeroVec);
        Gizmos.DrawLine(zeroVec, playRelative + zeroVec);
        dotProduct = ((trigRelative.x*playRelative.x)-(trigRelative.y*playRelative.y));
        //dotProduct = Vector2.Dot(trig, play);
        lookingAt = dotProduct >= threshold ? true : false;
        Gizmos.color = lookingAt ? Color.green : Color.red;
        Gizmos.DrawLine(Trigger.position, player.position);
    }
}