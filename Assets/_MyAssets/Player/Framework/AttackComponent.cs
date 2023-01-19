using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] CameraShaker _cameraShaker;
    [SerializeField] BoxCollider _boxColliderRef;
    [SerializeField] LayerMask EnemyLayerMask;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public virtual void Attack()
    {
        Collider[] colliders = Physics.OverlapBox(_boxColliderRef.gameObject.transform.position, _boxColliderRef.size / 2, Quaternion.identity, EnemyLayerMask);
        foreach (Collider col in colliders)
        {
            col.GetComponent<Enemy>().Death();
            _cameraShaker.ShakeCamera(0.5f, 0.1f);

            StartCoroutine(HitStun(.1f));
            GetComponent<MovementComponent>().ResetJump();
        }
    }

    IEnumerator HitStun(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
