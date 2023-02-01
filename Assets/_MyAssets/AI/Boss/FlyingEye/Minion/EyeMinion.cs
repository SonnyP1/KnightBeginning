using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMinion : Enemy
{
    [SerializeField] GameObject SpriteObj;
    [SerializeField] GameObject _projectileSpawnPoint;
    [SerializeField] GameObject _projectileToFire;
    [SerializeField] float _rateOfFire;
    [Header("Movement")]
    [SerializeField] float _speed;
    [SerializeField] float _maxXPos;


    private GameObject _playerObj;
    private float _moveFloat;

    private void Awake()
    {
        _playerObj = FindObjectOfType<PlayerController>().gameObject;
        StartCoroutine(FireProjectile());
    }

    IEnumerator FireProjectile()
    {
        yield return new WaitForSeconds(_rateOfFire);
        GameObject newProjectile = Instantiate(_projectileToFire,_projectileSpawnPoint.transform);
        newProjectile.transform.SetParent(null);
        StartCoroutine(FireProjectile());
    }

    private void Update()
    {
        LookAtPlayer();
    }
    private void LookAtPlayer()
    {
        Vector3 diff = _playerObj.transform.position - SpriteObj.transform.position;
        diff.Normalize();
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        SpriteObj.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90f);
    }

    void MoveMinion()
    {
        float yPos = gameObject.transform.position.y;
        float zPos = gameObject.transform.position.z;

        _moveFloat += Time.deltaTime;

        transform.position = new Vector3(0 + Mathf.Sin(_moveFloat * _speed) * _maxXPos, yPos, zPos);
    }
}
