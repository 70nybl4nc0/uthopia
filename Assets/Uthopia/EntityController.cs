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

    public new Rigidbody2D rigidbody => m_rigidbody;
    public new Collider2D collider => m_collider;
    public Vector3 position => transform.position;

    public UnityEvent<Collider2D> onCollisionEnter;
    public UnityEvent<Collider> onCollisionExit;

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
        m_target = null;
        SetColor(Color.white);
    }

    public void SetColor(Color color)
    {
        m_renderer.color = color;
    }

    public void FollowTarget(Transform target)
    {
        m_target = target;
    }


    public void MoveToPosition(Vector3 position)
    {
        m_rigidbody.MovePosition(position);
    }

    public void Move(Vector3 direction)
    {
        m_rigidbody.velocity = direction.normalized * speed;
    }

    private void FixedUpdate()
    {
        if (!m_target) return;

        var dirToTarget = (m_target.position - transform.position).normalized;
        var targetVelocity = dirToTarget * speed;

        rigidbody.velocity = Vector3.SmoothDamp(
            current: rigidbody.velocity,
            target: targetVelocity,
            currentVelocity: ref velocity,
            smoothTime: 0.1f,
            maxSpeed: speed,
            deltaTime: Time.fixedDeltaTime
            );
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
