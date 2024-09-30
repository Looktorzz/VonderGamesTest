using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    // TODO: use attack function from enemy

    [SerializeField]
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private float _detectionRange = 20f;

    [SerializeField]
    private float _attackRange = 5f;

    private const float _idlePointRadius = 0.1f;
    private const float _attackCooldown = 3f;

    private Player _player;
    private Transform _playerTransform;
    private Vector3 _idlePoint;
    private EnemyState _currentState = EnemyState.Idle;

    private void Start()
    {
        _idlePoint = this.transform.position;
        GameObject gameObject = GameObject.FindWithTag(GameConfig.PlayerTag);
        
        if (!gameObject.TryGetComponent<Player>(out _player))
        {
            Debug.LogAssertion($"Player not found in scene.");
        }
        else
        {
            _playerTransform = _player.transform;
        }

        StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.DetectPlayer:
                DetectPlayer();
                break;

            case EnemyState.WalkToPlayer:
                WalkToPlayer();
                break;

            case EnemyState.Attack:
                AttackPlayer();
                break;

            case EnemyState.ReturnToIdlePoint:
                ReturnToIdlePoint();
                break;
        }
    }

    private void Idle()
    {
        if (IsWithinDistance(transform.position, _playerTransform.position, _detectionRange))
        {
            _currentState = EnemyState.DetectPlayer;
        }
    }

    private void DetectPlayer()
    {
        if (!IsWithinDistance(transform.position, _playerTransform.position, _attackRange))
        {
            _currentState = EnemyState.WalkToPlayer;
        }
        else
        {
            _currentState = EnemyState.Attack;
        }
    }

    private void WalkToPlayer()
    {
        MoveTowardsTarget(_playerTransform.position);

        if (IsWithinDistance(transform.position, _playerTransform.position, _attackRange))
        {
            _currentState = EnemyState.Attack;
        }
        else if (!IsWithinDistance(transform.position, _playerTransform.position, _detectionRange))
        {
            _currentState = EnemyState.ReturnToIdlePoint;
        }
    }

    private void AttackPlayer()
    {
        if (!IsWithinDistance(transform.position, _playerTransform.position, _attackRange))
        {
            _currentState = EnemyState.WalkToPlayer;
        }
    }

    private void ReturnToIdlePoint()
    {
        MoveTowardsTarget(_idlePoint);

        if (IsWithinDistance(transform.position, _idlePoint, _idlePointRadius))
        {
            _currentState = EnemyState.Idle;
        }

        if (IsWithinDistance(transform.position, _playerTransform.position, _detectionRange))
        {
            _currentState = EnemyState.DetectPlayer;
        }
    }

    private void Attack()
    {
        Debug.Log("Attacking the player!");
    }

    private void MoveTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        _rigidbody.MovePosition(transform.position + direction * _speed * Time.deltaTime);
    }

    private bool IsWithinDistance(Vector3 positionA, Vector3 positionB, float radius)
    {
        return Vector3.Distance(positionA, positionB) <= radius;
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (_currentState == EnemyState.Attack)
            {
                Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }

            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
