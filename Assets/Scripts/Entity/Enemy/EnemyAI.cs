using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private BaseEnemy _baseEnemy;

    private Player _player;
    private Transform _playerTransform;
    private Vector3 _idlePoint;
    private EnemyState _currentState = EnemyState.Idle;

    private void Start()
    {
        _idlePoint = this.transform.position;
        _player = CoreGame.Instance.Player;
        _playerTransform = _player.transform;

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
        if (IsWithinDistance(transform.position, _playerTransform.position, _baseEnemy.DetectionRange))
        {
            _currentState = EnemyState.DetectPlayer;
        }
    }

    private void DetectPlayer()
    {
        if (!IsWithinDistance(transform.position, _playerTransform.position, _baseEnemy.AttackRange))
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
        _baseEnemy.MoveTowardsTarget(_playerTransform.position);

        if (IsWithinDistance(transform.position, _playerTransform.position, _baseEnemy.AttackRange))
        {
            _currentState = EnemyState.Attack;
        }
        else if (!IsWithinDistance(transform.position, _playerTransform.position, _baseEnemy.DetectionRange))
        {
            _currentState = EnemyState.ReturnToIdlePoint;
        }
    }

    private void AttackPlayer()
    {
        if (!IsWithinDistance(transform.position, _playerTransform.position, _baseEnemy.AttackRange))
        {
            _currentState = EnemyState.WalkToPlayer;
        }
    }

    private void ReturnToIdlePoint()
    {
        _baseEnemy.MoveTowardsTarget(_idlePoint);

        if (IsWithinDistance(transform.position, _idlePoint, BaseEnemy.IdlePointRadius))
        {
            _currentState = EnemyState.Idle;
        }

        if (IsWithinDistance(transform.position, _playerTransform.position, _baseEnemy.DetectionRange))
        {
            _currentState = EnemyState.DetectPlayer;
        }
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
                _baseEnemy.Attack(_player);
                yield return new WaitForSeconds(_baseEnemy.AttackCooldown);
            }

            yield return null;
        }
    }

    // For debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _baseEnemy.DetectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _baseEnemy.AttackRange);
    }
}
