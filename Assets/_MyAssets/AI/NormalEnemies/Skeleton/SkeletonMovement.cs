using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    private GameObject _player;
    private SimpleMove _simpleMoveComp;
    private Animator _animator;
    private bool _bisJumping = false;
    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
        _simpleMoveComp = GetComponent<SimpleMove>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!_bisJumping)
        {
            Vector3 target = (_player.transform.position - transform.position).normalized;
            if(Vector3.Dot(target,_player.transform.right) > 0)
            {
                Debug.Log("BEHIND PLAYER");
                _simpleMoveComp.enabled = false;
                _bisJumping=true;
                _animator.ResetTrigger("AttackTrigger");
                _animator.SetTrigger("JumpTrigger");
                _animator.SetBool("isFalling",true);
                StartCoroutine(FollowArc(transform.position,transform.position - (Vector3.left*1.5f),0.005f,2f));
            }
        }
    }

    IEnumerator FollowArc(
            Vector2 start,
            Vector2 end,
            float radius,
            float duration)
    {

        Vector2 difference = end - start;
        float span = difference.magnitude;

        // Override the radius if it's too small to bridge the points.
        float absRadius = Mathf.Abs(radius);
        if (span > 2f * absRadius)
            radius = absRadius = span / 2f;

        Vector2 perpendicular = new Vector2(difference.y, -difference.x) / span;
        perpendicular *= Mathf.Sign(radius) * Mathf.Sqrt(radius * radius - span * span / 4f);

        Vector2 center = start + difference / 2f + perpendicular;

        Vector2 toStart = start - center;
        float startAngle = Mathf.Atan2(toStart.y, toStart.x);

        Vector2 toEnd = end - center;
        float endAngle = Mathf.Atan2(toEnd.y, toEnd.x);

        // Choose the smaller of two angles separating the start & end
        float travel = (endAngle - startAngle + 5f * Mathf.PI) % (2f * Mathf.PI) - Mathf.PI;

        float progress = 0f;
        do
        {
            float angle = startAngle + progress * travel;
            Vector3 newPos = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * absRadius;
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
            progress += Time.deltaTime / duration;
            yield return null;
        } while (progress < 1f);

        transform.position = new Vector3(end.x,end.y,transform.position.z);

        //jump back finish
        _simpleMoveComp.enabled = true;
        _bisJumping = false;
        _animator.SetBool("isFalling",false);
    }
}
