using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeProjectile : MonoBehaviour
{
    [SerializeField] float speed;

    private Enemy closestEnemy;
    private void Start()
    {
        Debug.Log(LayerMask.NameToLayer("Enemy"));
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        float closestDistance = float.MaxValue;
        foreach(Enemy currentEnemy in allEnemies)
        {
            float distance = Vector3.Distance(gameObject.transform.position, currentEnemy.gameObject.transform.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = currentEnemy;
            }
        }

        StartCoroutine(MoveTowardsPlayer());
    }

    private void LookAtEnemy()
    {
        if(closestEnemy == null)
        {
            return;
        }

        Vector3 diff = closestEnemy.transform.position - gameObject.transform.position;
        diff.Normalize();
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }


    IEnumerator MoveTowardsPlayer()
    {
        while(true)
        {
            LookAtEnemy();
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Something");
        Debug.Log(other.gameObject.layer);
        Debug.Log(LayerMask.NameToLayer("Enemy"));
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("HitEnemy");
            other.GetComponent<Enemy>().Death();
            Destroy(gameObject);
        }
    }
}
