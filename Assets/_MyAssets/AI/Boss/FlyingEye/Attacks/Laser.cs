using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] LayerMask _playerMask;
    [SerializeField] float _laserTimer;
    [SerializeField] SpriteRenderer _laserSpriteRender;
    [SerializeField] BoxCollider _boxColliderRef;


    bool canHit = false;
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
        canHit = true;
        _boxColliderRef.enabled = true;

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<HealthComp>().Hit(1);
        }
    }
}
