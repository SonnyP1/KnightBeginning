using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(2f, 5f)]
    [SerializeField] float maxSpawnRate;
    [Range(2f, 0f)]
    [SerializeField] float minSpawnRate;
    [SerializeField] float bossSpawnTime;
    private float spawnRateChangeTime;

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
        if (CheckForBoss())
        {
            return;
        }


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
            Vector3 spawnLoc = new Vector3(transform.position.x,transform.position.y + yOffset, transform.position.z);
            Instantiate(Enemies[randomInt],spawnLoc,Quaternion.identity);


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
            //Debug.Log(bossTimer);
            if(bossTimer > bossSpawnTime)
            {
                SpawnBoss();
                bossTimer = 0f;
            }

            spawnRateChangeTime += Time.deltaTime;
            if(spawnRateChangeTime > 5f)
            {
                maxSpawnRate = Random.Range(2f, 5f);
                minSpawnRate = Random.Range(2f,0f);
                spawnRateChangeTime = 0f;
            }
        }
    }

    private bool CheckForBoss()
    {
        if(FindObjectOfType<Boss>())
        {
            return true;
        }
        return false;
    }

    public void SpawnTripleChestReward()
    {
        StartCoroutine(SpawnTripleChest());
    }
    private IEnumerator SpawnTripleChest()
    {
        yield return new WaitForSeconds(0.3f);
        SpawnChest();
        yield return new WaitForSeconds(0.3f);
        SpawnChest();
        yield return new WaitForSeconds(0.3f);
        SpawnChest();
    }
}
