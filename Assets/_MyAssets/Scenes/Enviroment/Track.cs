using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] BoxCollider floorCollider;
    private float _trackMovementSpeed;
    private float EndPointX;

    public void SetEndPointX(float newVal)
    {
        EndPointX = newVal;
    }
    public BoxCollider GetFloorCollider()
    {
        return floorCollider;
    }
    public void SetTrackMovementSpeed(float newSpeed)
    {
        _trackMovementSpeed = newSpeed;
    }

    private void Update()
    {
        transform.Translate(_trackMovementSpeed*Time.deltaTime, 0, 0);
        if(transform.position.x  < -10)
        {
            Destroy(gameObject);
        }
    }
}
