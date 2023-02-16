using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMangerSystem : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private Generator _gameGenerator;
    private void Start()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
        _gameGenerator = FindObjectOfType<Generator>();
    }
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
    private void StopTrack()
    {
        _gameGenerator.StopAllCoroutines();
        _gameGenerator.isPause = true;

        Track[] allTracker = FindObjectsOfType<Track>();
        foreach (Track track in allTracker)
        {
            track.SetTrackMovementSpeed(0.0f);
        }
    }

    private void StopEnemies()
    {
        _enemySpawner.isPause = true;
        _enemySpawner.StopAllCoroutines();
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in allEnemies)
        {
            if (enemy.GetComponent<SimpleMove>() != null)
            {
                enemy.GetComponent<SimpleMove>().StopMovement();
            }
        }

        Chest[] allChests = FindObjectsOfType<Chest>();
        foreach(Chest chest in allChests)
        {
            if(chest.GetComponent<SimpleMove>() != null)
            {
                chest.GetComponent<SimpleMove>().StopMovement();
            }
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
    private void ContinueTrack()
    {
        _gameGenerator.StartCoroutine(_gameGenerator.SpawnNewTrack());
        _gameGenerator.isPause = false;

        Track[] allTracker = FindObjectsOfType<Track>();
        foreach (Track track in allTracker)
        {
            track.SetTrackMovementSpeed(-1.5f);
        }
    }

    private void ContinueEnemies()
    {
        _enemySpawner.isPause = false;
        _enemySpawner.StartCoroutine(_enemySpawner.StartSpawning());

        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in allEnemies)
        {
            if(enemy.GetComponent<SimpleMove>() != null)
            {
                enemy.GetComponent<SimpleMove>().ContinueMovement();
            }
        }

        Chest[] allChests = FindObjectsOfType<Chest>();
        foreach (Chest chest in allChests)
        {
            if (chest.GetComponent<SimpleMove>() != null)
            {
                chest.GetComponent<SimpleMove>().ContinueMovement();
            }
        }
    }
}
