using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseEntity : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D Rigidbody2D;

    // TODO: Shoud have class for manage UI
    [SerializeField]
    protected TextMeshPro HpLabel;

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

        if (Hp <= 0)
        {
            Hp = 0;
            Dead();
        }

        HpLabel.text = Hp.ToString();
    }

    protected virtual void ResetHp()
    {
        Hp = MaxHp;
        HpLabel.text = Hp.ToString();
    }

    protected virtual void Dead()
    {
        Debug.Log($"{this.name} dead!");
        Destroy(this.gameObject);
    }
}
