using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public Vector3 reference;
    public GameObject EnemyPrefab;
    public int howmany = 3;
    public float gap = 10f;
    public float timewait = 3f;
    Vector3 ultimaPosizione;
    GameObject Player_;
    List<GameObject> Enemies = new List<GameObject>();
    bool spawning = false;
    void Start()
    {
        Player_ = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(StartSpawning());
    }

    void Update()
    {
        if (Enemies.Count != howmany)
            return;
        for (int x = 0; x < Enemies.Count; x++)
        {
            if (Enemies[x] == null && !spawning)
            {
                spawning = true;
                StartCoroutine(WaitSpawn(x));
            }
        }
    }
    public IEnumerator WaitSpawn(int id)
    {
        yield return new WaitForSeconds(timewait/2);
        Enemies[id] = AddSpawn();
        spawning = false;
    }
    public IEnumerator StartSpawning()
    {
        while (true)
        {
            if (Enemies.Count == howmany)
            {
                yield break;
            }
            yield return new WaitForSeconds(timewait);
            Enemies.Add(AddSpawn());
        }
    }
    public GameObject AddSpawn()
    {
        ultimaPosizione = new Vector3(Player_.transform.position.x - gap, reference.y, reference.z);
        return Instantiate(EnemyPrefab, ultimaPosizione, Quaternion.identity, transform);
    }
}
