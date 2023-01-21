using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float LaserAimTime;
    [SerializeField] float BossBreakTime;
    [SerializeField] float Health;
    private float MaxHealth;

    [Header("Projectile")]
    [SerializeField] Transform _projectileSpawnPoint;
    [SerializeField] GameObject _laserProjectile;
    private GameObject _playerObj;

    private void Start()
    {
        MaxHealth = Health;
        _playerObj = FindObjectOfType<PlayerController>().gameObject;
        ChooseAttackPattern();
    }

    void  ChooseAttackPattern()
    {
        int randomInt = Random.Range(0, 1);
        switch (randomInt)
        {
            case 0:
                StartCoroutine(FireLaser());
                break;
            case 1:
                StartCoroutine(FireLaser());
                break;
            default:
                StartCoroutine(BossBreak());
                break;
        }
    }


    IEnumerator BossBreak()
    {
        float time = 0.0f;
        while(time < BossBreakTime)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        ChooseAttackPattern();
    }

    IEnumerator FireLaser()
    {
        float time = 0.0f;
        while(time < LaserAimTime)
        {
            time += Time.deltaTime;
            LookAtPlayer();
            yield return new WaitForEndOfFrame();
        }
        FireProjectile(_laserProjectile);
        StartCoroutine(BossBreak());
    }

    void FireProjectile(GameObject projectileToSpawn)
    {
        GameObject newProjectile = Instantiate(projectileToSpawn, _projectileSpawnPoint);
        newProjectile.transform.parent = null;
    }
    private void LookAtPlayer()
    {
        Vector3 diff = _playerObj.transform.position - gameObject.transform.position;
        diff.Normalize();
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90f);
    }
}
