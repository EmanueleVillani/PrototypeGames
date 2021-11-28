using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public GatherInput gI;
    public GameObject playeranchor;
    public MoveByAnimation move;
    private void Start()
    {
        move = FindObjectOfType<MoveByAnimation>();
        Cursor.visible = false;
    }
    // Start is called before the first frame update
    void Update()
    {
        if (gI.mousedelta.magnitude != 0)
        {
            Vector3 pos = gI.mouseposition;
            pos.z = move.axis;
            Ray ray =  Camera.main.ScreenPointToRay(pos,Camera.main.stereoActiveEye);
            // transform.position = ray.GetPoint(pos.z); 
            //transform.position = Camera.main.ScreenToWorldPoint(pos);
            Vector3 par = Camera.main.ScreenToWorldPoint(pos);
            par.z = move.axis;
            Vector3 inter = Vector3.zero;
            if(LinePlaneIntersection(out inter,Camera.main.transform.position,ray.direction,Vector3.forward, playeranchor.transform.position))
            {
                transform.position = inter;
            }
        }
    }
    public bool LinePlaneIntersection(out Vector3 intersection, Vector3 linePoint, Vector3 lineVec, Vector3 planeNormal, Vector3 planePoint)
    {
        float length;
        float dotNumerator;
        float dotDenominator;
        Vector3 vector;
        intersection = Vector3.zero;

        //calculate the distance between the linePoint and the line-plane intersection point
        dotNumerator = Vector3.Dot((planePoint - linePoint), planeNormal);
        dotDenominator = Vector3.Dot(lineVec, planeNormal);

        if (dotDenominator != 0.0f)
        {
            length = dotNumerator / dotDenominator;

            vector = lineVec*length;

            intersection = linePoint + vector;
            Debug.DrawLine(Camera.main.transform.position,intersection);
            return true;
        }

        else
            return false;
    }
}
