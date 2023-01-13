using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] GameObject[] TrackToSpawn;
    [SerializeField] Transform EndSpawnPoint;
    [SerializeField] float Speed;
    private float _trackSize;
    void Start()
    {
        _trackSize = 3.59f;
        GenerateBeginningTiles();
    }

    void GenerateBeginningTiles()
    {
        float distanceToEnd = transform.position.x - EndSpawnPoint.position.x;
        int amountOfTilesNeeded = (int)(distanceToEnd / _trackSize);
        
        for(int i = 1; i <= amountOfTilesNeeded; i++)
        {
            float spawnLocX = EndSpawnPoint.position.x + i * _trackSize;
            Vector3 spawnLoc = new Vector3(spawnLocX, gameObject.transform.position.y, gameObject.transform.position.z);
            GameObject newTrack = Instantiate(TrackToSpawn[0], spawnLoc, Quaternion.identity);

            newTrack.GetComponent<Track>().SetTrackMovementSpeed(Speed);
        }

        StartCoroutine(SpawnNewTrack());
    }

    public IEnumerator SpawnNewTrack()
    {
        GameObject previousTrack = null;
        while(true)
        {
            Vector3 SpawnLoc = transform.position;

            GameObject newTrack = Instantiate(TrackToSpawn[0], SpawnLoc, Quaternion.identity);
            newTrack.GetComponent<Track>().SetTrackMovementSpeed(Speed);

            previousTrack = newTrack;
            float timeToGeneratorNextTrack = _trackSize / Speed*(-1);
            yield return new WaitForSeconds(timeToGeneratorNextTrack);
        }
    }
}
