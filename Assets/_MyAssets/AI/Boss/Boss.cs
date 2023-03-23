using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject _spriteObj;
    [SerializeField] GameObject _explosionObj;
    [SerializeField] GameObject _eyeMinion;


    [Header("Stats")]
    [SerializeField] Vector3 _bossStartLoc;
    [SerializeField] float _timeToStartBoss;
    [SerializeField] float LaserAimTime;
    [SerializeField] float BossBreakTime;
    [SerializeField] float _health;
    [SerializeField] float _speed;
    [SerializeField] float _maxXPos;



    [Header("Projectile")]
    [SerializeField] Transform _projectileSpawnPoint;
    [SerializeField] GameObject _laserProjectile;

    //private variables
    private bool isPause;
    private float _maxHealth;
    private float _moveFloat;
    private int _minionSpawn;
    private GameObject _playerObj;
    private bool isDead;

    private void Start()
    {
        _maxHealth = _health;
        _playerObj = FindObjectOfType<PlayerController>().gameObject;
        StartCoroutine(IntroBossScene());
    }

    IEnumerator IntroBossScene()
    {
        float time = 0f;
        Vector3 initialPos = transform.position;
        while(time < _timeToStartBoss)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(initialPos,_bossStartLoc, time/_timeToStartBoss);
            LookAtPlayer();
            yield return new WaitForEndOfFrame();
        }
        StartBoss();
    }

    public void StartBoss()
    {
        ChooseAttackPattern();
    }

    public virtual void  ChooseAttackPattern()
    {
        int randomInt = Random.Range(0, 2);
        switch (randomInt)
        {
            case 0:
                StartCoroutine(AttackPatternOne());
                break;
            case 1:
                StartCoroutine(AttackPatternTwo());
                break;
            default:
                StartCoroutine(BossBreak());
                break;
        }
    }


    public virtual IEnumerator BossBreak()
    {
        float time = 0.0f;

        while(time < BossBreakTime)
        {
            time += Time.deltaTime;
            HandleBossMovement();
            LookAtPlayer();
            yield return new WaitForEndOfFrame();
        }
        ChooseAttackPattern();
    }



    public virtual void HandleBossMovement()
    {
        float yPos = gameObject.transform.position.y;
        float zPos = gameObject.transform.position.z;

        _moveFloat += Time.deltaTime;

        transform.position = new Vector3(0 + Mathf.Sin(_moveFloat * _speed) * _maxXPos,yPos,zPos);
    }



    public virtual IEnumerator AttackPatternOne()
    {
        float time = 0.0f;
        while(time < LaserAimTime)
        {
            LookAtPlayer();
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        FireProjectile(_laserProjectile);

        yield return new WaitForSeconds(2f);
        StartCoroutine(BossBreak());
    }
    public virtual IEnumerator AttackPatternTwo()
    {
        _minionSpawn = 0;
        float time = 0.0f;
        while(time < 2f)
        {
            time += Time.deltaTime;
            LookAtPlayer();
            HandleBossMovement();
            yield return new WaitForEndOfFrame();
        }
        _minionSpawn++;
        FireProjectile(_eyeMinion);

        if(_minionSpawn == 3)
        {
            StartCoroutine(BossBreak());
        }
        else
        {
            StartCoroutine(AttackPatternTwo());
        }
    }

    public void FireProjectile(GameObject projectileToSpawn)
    {
        GameObject newProjectile = Instantiate(projectileToSpawn, _projectileSpawnPoint);
        newProjectile.transform.parent = null;
    }
    private void LookAtPlayer()
    {
        Vector3 diff = _playerObj.transform.position - _spriteObj.transform.position;
        diff.Normalize();
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        _spriteObj.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90f);
    }

    internal void Hit()
    {
        _health = Mathf.Clamp(_health - 1, 0, _health);
        if (_health <= 0)
        {
            if (!isDead)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        isDead = true;
        StopAllCoroutines();
        StartCoroutine(DeathScene());
    }

    public virtual IEnumerator DeathScene()
    {
        float time = 0.0f;
        Vector3 initialPos = transform.position;
        float amountX = 0.03f;
        float amountY = 0.01f;
        float speed = 20f;
        StartCoroutine(SpawnExplosionsForDeath());

        while (time < _timeToStartBoss)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(initialPos, _bossStartLoc, time / _timeToStartBoss);
            _spriteObj.transform.position = new Vector3 ( initialPos.x + Mathf.Sin(Time.time * speed) * amountX, initialPos.y +Mathf.Sin(Time.time * speed)* amountY, initialPos.z);
            yield return new WaitForEndOfFrame();
        }

        FindObjectOfType<EnemySpawner>().SpawnTripleChestReward();
        Destroy(gameObject);
    }

    IEnumerator SpawnExplosionsForDeath()
    {
        GameObject explosion = Instantiate(_explosionObj, _spriteObj.transform);
        explosion.transform.position = new Vector3(_spriteObj.transform.position.x + Random.Range(-0.5f,0.5f),
            _spriteObj.transform.position.y + Random.Range(-0.5f,0.5f),
            _spriteObj.transform.position.z);
        explosion.transform.parent = null;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(SpawnExplosionsForDeath());
    }
}
