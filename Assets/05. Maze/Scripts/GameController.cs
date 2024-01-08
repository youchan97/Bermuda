using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public interface IHitable //�ǰ� ��ü
{
    void Hit(int damage);
}
public interface IAttackable //���� ��ü
{
    void Attack(IHitable hitable);
}

public interface IUseable
{
    void Use();

    IEnumerator ReturnCool(float time);
}
public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    //private float speedUpValue = 1f;
    public GameObject DirectionUi;
    public GameObject invisibleUI;
    public Inventory inventory;
    public PlayerManager player;

    //�� �������� ���� �ð� ���� �� ���� �ð� ���� �� �� ���� ���� 
    /*IEnumerator SpeedCoolTime(float _time)
    {
        yield return new WaitForSeconds(_time);
        player.moveSpeed -= speedUpValue;
    }
    IEnumerator InvisibleCoolTime(float _time)
    {
        invisibleUI.SetActive(true);
        yield return new WaitForSeconds(_time);
        invisibleUI.SetActive(false);
        player.isInvisible = false;
    }
    IEnumerator SprayCoolTime(float _time)
    {
        yield return new WaitForSeconds(_time);
        DirectionUi.SetActive(false);
    }*/

    IEnumerator Wait(float time, int index)
    {
        yield return new WaitForSeconds(time);
        Debug.Log(inventory.slots[index].item.runningTime);
        ItemNull(index);
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DirectionUi.SetActive(false);
        inventory = FindObjectOfType<Inventory>();
        player = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SlotCheck(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SlotCheck(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SlotCheck(2);
        }
        if( Input.GetKeyDown(KeyCode.Alpha4))
        {
            SlotCheck(3);
        }
    }
    public void SlotCheck(int index) //�κ��丮�� �������� ����ֳ� Ȯ��
    {
        if(inventory.slots[index].item != null)
        {
            inventory.slots[index].item.Use(); //��������� ���
            StartCoroutine(Wait(inventory.slots[index].item.runningTime, index));
        }
        else
        {
            Debug.Log((index+1) + "�� ����Ű�� ��� �� �� �ִ� �������� �����ϴ�.");
        }
    }
    /*private void UseItem(int index) 
    {
        //������ �������� ����ִ� �̸� �Ӽ����� �������� ������ �Ǵ���
        //�´� �������� ȿ���� runningTime���� �ߵ�
        if (inventory.slots[index].item.name == "SpeedUp")
        {
            float runningTime = 5f;
            player.moveSpeed += speedUpValue; // 
            ItemNull(index);
            StartCoroutine(SpeedCoolTime(runningTime));
        }
        else if (inventory.slots[index].item.name == "Invisible")
        {
            float runningTime = 3f;
            player.isInvisible = true; // �÷��̾��� ���� ���� ����
            ItemNull(index);
            StartCoroutine(InvisibleCoolTime(runningTime));
        }
        else if (inventory.slots[index].item.name == "RouteSpray")
        {
            float runningTime = 5f;
            DirectionUi.SetActive(true); // �ùٸ� �� UI ����
            ItemNull(index);
            StartCoroutine(SprayCoolTime(runningTime));
        }

        inventory.RemoveAt(index); //������� �� �κ��丮 ĭ �迭 �����
    }*/

    public void ItemNull(int index) //���� �������� Nulló��
    {
        inventory.slots[index].item = null;
    }
}
