using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(2f, 5f)]
    [SerializeField] float maxSpawnRate;
    [Range(2f, 1f)]
    [SerializeField] float minSpawnRate;
    [SerializeField] float bossSpawnTime;

    [SerializeField] float yGroundSpawn;
    [SerializeField] GameObject[] Enemies;
    [SerializeField] GameObject[] Bosses;
    [SerializeField] GameObject Chest;
    public bool isPause = false;

    private float chestTimer;
    private float bossTimer;

    private void Start()
    {
        //StartCoroutine(StartSpawning());
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
            if(!Enemies[randomInt].GetComponent<Enemy>().IsGroundedEnemy())
            {
                yOffset = Random.Range(0.5f, 1f);
            }
            Vector3 spawnLoc = new Vector3(transform.position.x,transform.position.y+ yGroundSpawn + yOffset, transform.position.z);
            GameObject newEnemy = Instantiate(Enemies[randomInt],spawnLoc,Quaternion.identity);
            newEnemy.GetComponent<SimpleMove>();

            yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
        }
    }

    void SpawnBoss()
    {
        float yOffset = Random.Range(0.5f, 1f);
        Vector3 spawnLoc = new Vector3(transform.position.x,transform.position.y+ yGroundSpawn + yOffset, transform.position.z);
        int randomInt = Random.Range(0, Bosses.Length);
        Instantiate(Bosses[randomInt],spawnLoc,Quaternion.identity);
    }

    private void Update()
    {
        if(!isPause)
        {
            chestTimer += Time.deltaTime;
            if(chestTimer > 5f)
            {
                SpawnChest();
                chestTimer = 0f;
            }

            bossTimer += Time.deltaTime;

            if(bossTimer > bossSpawnTime)
            {
                SpawnBoss();
                bossTimer = 0f;
            }
        }
    }
}
