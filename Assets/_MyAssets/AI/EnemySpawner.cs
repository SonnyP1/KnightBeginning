using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.5f,5f)]
    [SerializeField] float SpawnRate;
    [SerializeField] float yGroundSpawn;
    [SerializeField] GameObject[] Enemies;
    [SerializeField] GameObject Chest;

    private float chestTimer;

    private void Start()
    {
        StartCoroutine(StartSpawning());

        SpawnChest();
    }

    private void SpawnChest()
    {
        Vector3 spawnLoc = new Vector3(transform.position.x, -0.8f, transform.position.z);
        Instantiate(Chest, spawnLoc, Quaternion.identity);
    }

    public IEnumerator StartSpawning()
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


    private void Update()
    {
        chestTimer += Time.deltaTime;
        if(chestTimer > 30f)
        {
            SpawnChest();
            chestTimer = 0f;
        }
    }
}
