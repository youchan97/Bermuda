using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maze
{
    public class Trap : MonoBehaviour, IAttackable
    {

        [SerializeField] protected int damage;
        public float speed;
        protected TrapStrategy trapStrategy;
        private void Start()
        {
            damage = 5;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && other.GetComponent<PlayerManager>().isInvisible == false)
            {
                if (other.TryGetComponent(out IHitable hitable)) //피격체가 IHitable을 가지고 있는지 검사
                {
                    Attack(hitable);
                }
            }
        }

        public void Attack(IHitable hitable)
        {
            hitable.Hit(damage); //피격체의 Hit()함수 실행
        }
    }
}
