using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.5f,5f)]
    [SerializeField] float SpawnRate;
    [SerializeField] float yGroundSpawn;
    [SerializeField] GameObject[] Enemies;


    private void Start()
    {
        StartCoroutine(StartSpawning());
    }
    IEnumerator StartSpawning()
    {
        while(true)
        {
            Vector3 spawnLoc = new Vector3(transform.position.x,transform.position.y+yGroundSpawn, transform.position.z);
            Instantiate(Enemies[0],spawnLoc,Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0.5f, SpawnRate));
        }
    }
}
