using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EntityController : MonoBehaviour
{
    private SpriteRenderer m_renderer;
    private Collider2D m_collider;
    private Rigidbody2D m_rigidbody;

    public float speed;
    Vector3 velocity = Vector3.zero;
    Transform m_target;
    Vector3? m_targetPosition;

    public new Rigidbody2D rigidbody => m_rigidbody;
    public new Collider2D collider => m_collider;
    public Vector3 position => transform.position;

    public UnityEvent<Collider2D> onCollisionEnter;
    public UnityEvent<Collider> onCollisionExit;
    public UnityEvent onReachTarget;

    private void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Reset()
    {
        onCollisionEnter.RemoveAllListeners();
        onCollisionExit.RemoveAllListeners();
        onReachTarget.RemoveAllListeners();
        transform.localScale = new Vector3(1, 1, 1);
        velocity = Vector3.zero;
        m_target = null;
        m_targetPosition = null;
        speed = 0;
        SetColor(Color.white);
        StopAllCoroutines();
    }

    public void SetColor(Color color)
    {
        m_renderer.color = color;
    }

    public void FollowTarget(Transform target, UnityAction onReachTarget = null)
    {
        m_target = target;
        if (onReachTarget != null)
            this.onReachTarget.AddListener(onReachTarget);
    }

    public void FollowPosition(Vector3 target, UnityAction onReachTarget = null)
    {
        m_targetPosition = target;
        if (onReachTarget != null)
            this.onReachTarget.AddListener(onReachTarget);
    }


    public void MoveToPosition(Vector3 position)
    {
        m_rigidbody.MovePosition(position);
    }

    public void Move(Vector3 direction)
    {
        m_rigidbody.velocity = direction.normalized * speed;
    }

    private Vector3? getTargetPosition()
    {

        Vector3? targetPosition = null;
        if (m_target)
            targetPosition = m_target.position;
        else if (m_targetPosition != null)
            targetPosition = m_targetPosition;

        return targetPosition;
    }

    private void FixedUpdate()
    {
        Vector3? targetPosition = getTargetPosition();

        if (targetPosition == null) return;

        var diff = targetPosition.Value - transform.position;
        var dirToTarget = diff.normalized;

        m_rigidbody.velocity = dirToTarget * speed;
    }

    private void Update()
    {
        Vector3? targetPosition = getTargetPosition();

        if (targetPosition == null) return;

        var diff = targetPosition.Value - transform.position;

        if (diff.magnitude <= Time.deltaTime * m_rigidbody.velocity.magnitude)
            onReachTarget.Invoke();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        onCollisionEnter.Invoke(collision);
    }

    private void OnTriggerExit(Collider other)
    {
        onCollisionExit.Invoke(other);
    }

}
