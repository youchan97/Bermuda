using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public event Action OnGameEnd;

    //�� �������� ���� �ð� ���� �� ���� �ð� ���� �� �� ���� ���� 

    IEnumerator Wait(float time, int index)
    {
        yield return new WaitForSeconds(time);
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
            Debug.Log((int)KeyCode.Alpha1);
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
        if (player.GameHp <= 0)
            OnGameEnd();
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

    public void ItemNull(int index) //���� �������� Nulló��
    {
        inventory.slots[index].item = null;
    }
}
