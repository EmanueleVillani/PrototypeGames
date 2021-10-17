using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunControls : MonoBehaviour
{
    // Start is called before the first frame update
    private GatherInput gI;
    public GameObject gun;
    public GameObject projectile;
    public Transform spawn;
    Quaternion direction;
    void Start()
    {
        gI = GetComponent<GatherInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gI.mousedelta.magnitude!=0)
        {
            gun.transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(gI.mouseposition.x, gI.mouseposition.y, transform.position.z)),Vector3.forward);//guarda il punto mouseposition con perno vector3.forward
        }
        if (gI.fireInput)
        {
            Shot();
        }
    }
    public void Shot()
    {
        Instantiate<GameObject>(projectile, spawn.position, spawn.rotation);
    }
}
