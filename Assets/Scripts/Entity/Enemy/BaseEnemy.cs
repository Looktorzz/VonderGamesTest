using UnityEngine;

public abstract class BaseEnemy : BaseEntity
{
    [SerializeField]
    private int _damage = 3;
    public int Damage => _damage;

    [SerializeField]
    private float _detectionRange = 5f;
    public float DetectionRange => _detectionRange;

    [SerializeField]
    private float _attackRange = 2f;
    public float AttackRange => _attackRange;

    [SerializeField]
    private float _attackCooldown = 3f;
    public float AttackCooldown => _attackCooldown;

    public const float IdlePointRadius = 0.1f;

    public abstract void Attack(Player player);

    public virtual void MoveTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Rigidbody2D.MovePosition(transform.position + direction * Speed * Time.deltaTime);
    }
}
