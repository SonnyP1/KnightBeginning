using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMangerSystem : MonoBehaviour
{
    public void StopGame()
    {
        StopPlayer();
        StopEnemies();
        StopTrack();
    }

    private static void StopPlayer()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        if (player != null)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<MovementComponent>().enabled = false;
            player.GetComponent<PlayerController>().DisableGameplayInputs();
        }

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
        enemySpawner.isPause = true;
        enemySpawner.StopAllCoroutines();
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in allEnemies)
        {
            enemy.enabled = false;
        }
    }

    public void ContinueGame()
    {
        ContinuePlayer();
        ContinueEnemies();
        ContinueTrack();
    }

    private static void ContinuePlayer()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        if (player != null)
        {
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponent<MovementComponent>().enabled = true;
            player.GetComponent<PlayerController>().EnabledGameplayInputs();
        }
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
        enemySpawner.isPause = false;
        enemySpawner.StartCoroutine(enemySpawner.StartSpawning());
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in allEnemies)
        {
            enemy.enabled = true;
        }
    }
}
