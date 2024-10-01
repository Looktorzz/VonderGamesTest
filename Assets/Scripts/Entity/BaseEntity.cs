using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseEntity : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D Rigidbody2D;

    [SerializeField]
    protected int MaxHp = 20;

    [SerializeField]
    protected float Speed = 5f;

    protected int Hp;

    public void SetUp()
    {
        ResetHp();
    }

    public void Hurt(int damage)
    {
        ReduceHp(damage);
    }

    protected virtual void ReduceHp(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            Hp = 0;
            Dead();
        }
    }

    protected virtual void ResetHp()
    {
        Hp = MaxHp;
    }

    protected virtual void Dead()
    {
        Destroy(this.gameObject);
    }
}
