using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    protected virtual void OnTrap(PlayerHp player)
    {
        player.CurrentHp--;

        // �� ������ ��� �߰�
        // ������ �÷�� ���ư��ٴ���...
        // �̵��ӵ��� �������ٴ���...
    }
    protected void OnTriggerEnter(Collider other)
    {
        PlayerHp pl;
        if (other.TryGetComponent<PlayerHp>(out pl))
        {
            OnTrap(pl);
        }
    }
}
