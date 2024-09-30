using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    private const float _speed = 5;
    private const float _minDamage = 3;
    private const float _maxDamage = 5;

    private Vector2 _directionMove;

    private void Update()
    {
        _rigidbody2D.velocity = _directionMove * _speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: if other is enemy hurt enemy.
        // TODO: hurt enemy by random damage 3-5.
        Destroy(this.gameObject);
    }

    public void Setup(Vector2 direction)
    {
        _directionMove = direction;
    }
}
