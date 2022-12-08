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
            int randomInt = Random.Range(0, Enemies.Length);
            float yOffset = 0.0f;
            if(randomInt != 0 )
            {
                yOffset = Random.Range(0.5f, 1f);
            }
            Vector3 spawnLoc = new Vector3(transform.position.x,transform.position.y+ yGroundSpawn + yOffset, transform.position.z);
            Instantiate(Enemies[randomInt],spawnLoc,Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0.5f, SpawnRate));
        }
    }
}
