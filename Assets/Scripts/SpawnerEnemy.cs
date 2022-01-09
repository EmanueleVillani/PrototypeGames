using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    Vector2 ultimaPosizione;
    public GameObject EnemyPrefab;
    public GameObject CurrentEnemy;
    public GameObject Player_;
    public GameObject CurrentEnemy2;
    public GameObject CurrentEnemy3;
   




    void Start()
    {


    }


    void Update()

    {

        SpawnEnemy();

    }
    void SpawnEnemy()
    {

        ultimaPosizione = new Vector2(Player_.transform.position.x * 2 * Time.deltaTime * +25, transform.position.y);
        if (!CurrentEnemy )
        {
            CurrentEnemy = Instantiate(EnemyPrefab, ultimaPosizione, Quaternion.identity);
        }
        else if (!CurrentEnemy2)
        {
            CurrentEnemy2 = Instantiate(EnemyPrefab, ultimaPosizione, Quaternion.identity);
        }

        else if (!CurrentEnemy3)
        {
            CurrentEnemy3 = Instantiate(EnemyPrefab, ultimaPosizione, Quaternion.identity);
        }
      
    }

}
