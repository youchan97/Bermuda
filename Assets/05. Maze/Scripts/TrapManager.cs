using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IHitable
{
    void Hit(int damage);
}
public interface IAttackable
{
    void Attack(IHitable hitable);
}

public class TrapManager : MonoBehaviour, IAttackable
{

    [SerializeField]protected int damage;
    [SerializeField]protected float speed;
    private void Start()
    {
        damage = 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerManager>().isInvisible == false)
        {
            if(other.TryGetComponent(out IHitable hitable))
            {
                Attack(hitable);
            }
        }
    }

    public void Attack(IHitable hitable)
    {
        hitable.Hit(damage);
    }
}
