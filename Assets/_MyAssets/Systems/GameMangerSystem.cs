using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMangerSystem : MonoBehaviour
{
    public void StopGame()
    {
        FindObjectOfType<MovementComponent>().enabled = false;
        StopEnemies();
        StopTrack();
    }

    private static void StopTrack()
    {
        Generator gameGenerator = FindObjectOfType<Generator>();
        gameGenerator.enabled = false;
        gameGenerator.StopAllCoroutines();

        Track[] allTracker = FindObjectsOfType<Track>();
        foreach (Track track in allTracker)
        {
            track.SetTrackMovementSpeed(0.0f);
        }
    }

    private void StopEnemies()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.StopAllCoroutines();
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in allEnemies)
        {
            enemy.enabled = false;
        }
    }


    public void ContinueGame()
    {
        FindObjectOfType<MovementComponent>().enabled = true;


        ContinueEnemies();
        ContinueTrack();
    }

    private static void ContinueTrack()
    {
        Generator gameGenerator = FindObjectOfType<Generator>();
        gameGenerator.enabled = true;
        gameGenerator.StartCoroutine(gameGenerator.SpawnNewTrack());


        Track[] allTracker = FindObjectsOfType<Track>();
        foreach (Track track in allTracker)
        {
            track.SetTrackMovementSpeed(-1.5f);
        }
    }

    private static void ContinueEnemies()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.StartCoroutine(enemySpawner.StartSpawning());
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in allEnemies)
        {
            enemy.enabled = true;
        }
    }
}
