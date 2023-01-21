using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] LayerMask _playerMask;
    [SerializeField] float _laserTimer;
    [SerializeField] SpriteRenderer _laserSpriteRender;
    [SerializeField] BoxCollider _boxColliderRef;

    private void Start()
    {
        StartCoroutine(StartLaserAttack());
    }

    IEnumerator StartLaserAttack()
    {
        Color tmpColor = _laserSpriteRender.color;
        tmpColor.a = 0;
        _laserSpriteRender.color = tmpColor;

        yield return new WaitForSeconds(_laserTimer/5);
        _laserSpriteRender.color = Color.white;

        yield return new WaitForSeconds(_laserTimer/5);
        _laserSpriteRender.color = tmpColor;
        

        yield return new WaitForSeconds(_laserTimer/5);
        _laserSpriteRender.color = Color.white;    

        yield return new WaitForSeconds(_laserTimer/5);
        _laserSpriteRender.color = tmpColor;        
        
        yield return new WaitForSeconds(_laserTimer/5);
        _laserSpriteRender.color = Color.white;
    

        //Do Overlap here
        SpawnHitBox();

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void SpawnHitBox()
    {
        Collider[] colliders = Physics.OverlapBox(_boxColliderRef.gameObject.transform.position + _boxColliderRef.center,
            _boxColliderRef.size, 
            transform.rotation, _playerMask);

        foreach (Collider col in colliders)
        {
            HealthComp playerHealth = col.gameObject.GetComponent<HealthComp>();
            if (playerHealth.GetCurrentHealth() > 0)
            {
                playerHealth.Hit(1, true);
                FindObjectOfType<CameraShaker>().ShakeCamera(1f, 0.1f);
                if (playerHealth.GetCurrentHealth() > 0)
                {
                    StartCoroutine(HitStun(.2f));
                }
            }
        }
    }

    IEnumerator HitStun(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
