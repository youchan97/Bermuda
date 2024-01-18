using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public interface IHitable //피격 객체
{
    void Hit(int damage);
}
public interface IAttackable //공격 객체
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

    //각 아이템의 지속 시간 생성 및 지속 시간 종료 시 원 상태 복귀 

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
    public void SlotCheck(int index) //인벤토리에 아이템이 들어있나 확인
    {
        if(inventory.slots[index].item != null)
        {
            inventory.slots[index].item.Use(); //들어있으면 사용
            StartCoroutine(Wait(inventory.slots[index].item.runningTime, index));
        }
        else
        {
            Debug.Log((index+1) + "번 단축키로 사용 할 수 있는 아이템이 없습니다.");
        }
    }

    public void ItemNull(int index) //사용된 아이템을 Null처리
    {
        inventory.slots[index].item = null;
    }
}
