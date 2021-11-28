using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStalker : MonoBehaviour
{
    GameObject player;
    MoveByAnimation movements;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movements = player.GetComponent<MoveByAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        if (movements.isRight)
            transform.forward = Vector3.right;
        else
            transform.forward = Vector3.left;

    }
}
