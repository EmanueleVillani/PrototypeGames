using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetGenerator : MonoBehaviour
{
    public GameObject street1, street2;
    public GameObject player;
    Bounds newbound;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer[] renderers = street1.GetComponentsInChildren<MeshRenderer>();
        newbound = new Bounds(Vector3.zero, Vector3.zero);
        foreach (MeshRenderer rend in renderers)
        {
            newbound.Encapsulate(rend.bounds);
        }
        Debug.Log(newbound.size);
        Debug.Log(newbound.max);
        Debug.Log(newbound.min);
    }
    // Update is called once per frame
    void Update()
    {
        float dist1 = Vector3.Distance(player.transform.position, street1.transform.position);
        float dist2 = Vector3.Distance(player.transform.position, street2.transform.position);
       //Debug.Log(dist1);
       //Debug.Log(dist2);
        if (dist1 > newbound.size.x)
        {
            Vector3 dir = player.transform.position - street1.transform.position;
            street1.transform.position = street1.transform.position + Vector3.ProjectOnPlane(dir.normalized, Vector3.up).normalized * 2 * newbound.size.x;
        }
        else if (dist2 > newbound.size.x)
        {
            Vector3 dir = player.transform.position - street2.transform.position;
            street2.transform.position = street2.transform.position + Vector3.ProjectOnPlane(dir.normalized, Vector3.up).normalized * 2 * newbound.size.x;
        }
    }
}
