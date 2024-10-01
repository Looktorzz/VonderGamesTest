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

    private void Start()
    {
        SetUp(); 
    }

    public virtual void SetUp()
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
        Debug.Log($"{this.name} hp remaining : {Hp}");

        if (Hp <= 0)
        {
            Hp = 0;
            Dead();
        }
    }

    protected virtual void ResetHp()
    {
        Hp = MaxHp;
        Debug.Log($"{this.name} Reset to max hp : {Hp}/{MaxHp}");
    }

    protected virtual void Dead()
    {
        Debug.Log($"{this.name} dead!");
        Destroy(this.gameObject);
    }
}
