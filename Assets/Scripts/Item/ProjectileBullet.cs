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
    private const int _minDamage = 3;
    private const int _maxDamage = 5;

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
        if (other.TryGetComponent(out BaseEnemy enemy))
        {
            int damage = Random.Range(_minDamage, _maxDamage + 1);
            enemy.Hurt(damage);
            Destroy(this.gameObject);
        }
    }

    public void Setup(bool isLeftDirection)
    {
        _directionMove = new Vector2();
        _directionMove.x = isLeftDirection ? -1 : 1;
    }
}
