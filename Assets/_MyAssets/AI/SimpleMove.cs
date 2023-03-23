using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    [SerializeField] float _movementSpeed;
    private float _orginalSpeed;

    private void Awake()
    {
        _orginalSpeed = _movementSpeed;
    }
    public void ContinueMovement()
    {
        _movementSpeed = _orginalSpeed;
    }
    public void StopMovement()
    {
        _movementSpeed= 0;
    }

    private void Update()
    {
        UpdateMovement();
    }
    public virtual void UpdateMovement()
    {
        transform.Translate(_movementSpeed * Time.deltaTime, 0, 0);
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
}
