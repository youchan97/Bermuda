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
                if (other.TryGetComponent(out IHitable hitable)) //�ǰ�ü�� IHitable�� ������ �ִ��� �˻�
                {
                    Attack(hitable);
                }
            }
        }

        public void Attack(IHitable hitable)
        {
            hitable.Hit(damage); //�ǰ�ü�� Hit()�Լ� ����
        }
    }
}
