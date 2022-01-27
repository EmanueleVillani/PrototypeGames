using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
//<<<<<<< Updated upstream
    Vector3 ultimaPosizione;
//=======
//    Vector3 ultimaPosizione = new Vector3(5.5f, 0, 0);
//>>>>>>> Stashed changes
    public GameObject EnemyPrefab;
    public GameObject CurrentEnemy;
    public GameObject Player_;
    public GameObject CurrentEnemy2;
    public GameObject CurrentEnemy3;
    Transform enemyparent;
    Vector3 reference ;
    public float gap = 10f;
    void Start()
    {
        enemyparent = CurrentEnemy.transform.parent;
        reference = CurrentEnemy.transform.position;
    }


    void Update()
    {
        SpawnEnemy();
    }
    void SpawnEnemy()
    {
//<<<<<<< Updated upstream
        if (CurrentEnemy==null)
//=======
//
//        ultimaPosizione = new Vector2(Player_.transform.position.x * 2 * Time.deltaTime * +25, transform.position.y);
//        //ultimaPosizione = Player_.transform.position * 10+ Player_.transform.position + new Vector3(5.5f, 0, 0);
//        if (!CurrentEnemy )
//>>>>>>> Stashed changes
        {
            ultimaPosizione = new Vector3(Player_.transform.position.x- gap, reference.y, reference.z);
            CurrentEnemy = Instantiate(EnemyPrefab, ultimaPosizione, Quaternion.identity, enemyparent);
        }
        else if (!CurrentEnemy2)
        {
            ultimaPosizione = new Vector3(Player_.transform.position.x- (gap + 10f), reference.y, reference.z);
            CurrentEnemy2 = Instantiate(EnemyPrefab, ultimaPosizione, Quaternion.identity, enemyparent);
        }
        else if (!CurrentEnemy3)
        {
            ultimaPosizione = new Vector3(Player_.transform.position.x- (gap + 20f), reference.y, reference.z);
            CurrentEnemy3 = Instantiate(EnemyPrefab, ultimaPosizione, Quaternion.identity, enemyparent);
        }
        // ultimaPosizione = new Vector2(Player_.transform.position.x * 2 * Time.deltaTime * +25, transform.position.y);
        // if (!CurrentEnemy )
        // {
        //     CurrentEnemy = Instantiate(EnemyPrefab, ultimaPosizione, Quaternion.identity);
        // }
        // else if (!CurrentEnemy2)
        // {
        //     CurrentEnemy2 = Instantiate(EnemyPrefab, ultimaPosizione, Quaternion.identity);
        // }
        //
        // else if (!CurrentEnemy3)
        // {
        //     CurrentEnemy3 = Instantiate(EnemyPrefab, ultimaPosizione, Quaternion.identity);
        // }

    }

}
