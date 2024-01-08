using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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

    //각 아이템의 지속 시간 생성 및 지속 시간 종료 시 원 상태 복귀 
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
    /*private void UseItem(int index) 
    {
        //각각의 아이템의 담겨있는 이름 속성으로 아이템의 종류를 판단해
        //맞는 아이템의 효과가 runningTime동안 발동
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
            player.isInvisible = true; // 플레이어의 무적 판정 조건
            ItemNull(index);
            StartCoroutine(InvisibleCoolTime(runningTime));
        }
        else if (inventory.slots[index].item.name == "RouteSpray")
        {
            float runningTime = 5f;
            DirectionUi.SetActive(true); // 올바른 길 UI 생성
            ItemNull(index);
            StartCoroutine(SprayCoolTime(runningTime));
        }

        inventory.RemoveAt(index); //사용했을 때 인벤토리 칸 배열 비워둠
    }*/

    public void ItemNull(int index) //사용된 아이템을 Null처리
    {
        inventory.slots[index].item = null;
    }
}
