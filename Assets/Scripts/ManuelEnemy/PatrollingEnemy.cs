using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : Enemy2D
{
    public float speed;// la velocità dello zombie 
    public int direction = 1; // direzione zombie

    //Crea anche due variabili palla per memorizzare le informazioni.
    private bool dateGround;
    private bool datecWall;

    public float radius; 
    //imposta la direzione su meno uno perché il
    //nemico per impostazione predefinita guarda
    //a sinistra, quindi dobbiamo trasformare
    public Transform wallChec;
    public Transform groundCheck;
    public LayerMask layerTocheck;

    void Start()
    {
        
    }
    

    void FixedUpdate()
    {
        Flip();
        rb.velocity = new Vector2(direction*speed, rb.velocity.y);
    }
    private void Flip()
    {
        //Archiviamo più informazioni nel terreno per rilevatore per creare un cerchio che controllerà il terreno,
        dateGround = Physics2D.OverlapCircle(groundCheck.position, radius, layerTocheck);
        datecWall = Physics2D.OverlapCircle(wallChec.position, radius, layerTocheck);


        //Quindi controlliamo se all'interno
        //del cerchio abbiamo rilevato un muro
        //o non abbiamo rilevato terreno sottostante.
        if (datecWall || dateGround == false)
        {
            direction *= -1; //Quindi moltiplicare la direzione con meno uno per girare intorno al nemico.
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radius);
        Gizmos.DrawSphere(wallChec.position, radius);

    }
}
