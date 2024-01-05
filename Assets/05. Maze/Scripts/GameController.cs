using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private float speedUpValue = 1f;
    public GameObject DirectionUi;
    public GameObject invisibleUI;
    bool isPause;
    public Inventory inventory;
    public PlayerManager player;
    IEnumerator SpeedCoolTime(float _time)
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
    }

    private void Start()
    {
        //SceneManager.LoadScene("Start");
        isPause = false;
        DirectionUi.SetActive(false);
        inventory = FindObjectOfType<Inventory>();
        player = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (isPause == false)
        //    {
        //        Time.timeScale = 0;
        //        isPause = true;
        //        return;
        //    }
        //    if(isPause == true)
        //    {
        //        Time.timeScale = 1;
        //        isPause = false;
        //        return;
        //    }
        //}
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
    private void SlotCheck(int index)
    {
        if(inventory.slots[index].item != null)
        {
            UseItem(index);
        }
        else
        {
            Debug.Log((index+1) + "번 단축키로 사용 할 수 있는 아이템이 없습니다.");
        }
    }
    private void UseItem(int index) //아이템이 사용되었을 시 빈 공간 이미지 불러오기 구현
    {
        //inventory.slots[index].Use();

        if (inventory.slots[index].item.name == "SpeedUp")
        {
            float coolTime = 5f;
            player.moveSpeed += speedUpValue;
            ItemNull(index);
            StartCoroutine(SpeedCoolTime(coolTime));
        }
        else if (inventory.slots[index].item.name == "Invisible")
        {
            float coolTime = 3f;
            player.isInvisible = true;
            ItemNull(index);
            StartCoroutine(InvisibleCoolTime(coolTime));
        }
        else if (inventory.slots[index].item.name == "RouteSpray")
        {
            float coolTime = 5f;
            DirectionUi.SetActive(true);
            ItemNull(index);
            StartCoroutine(SprayCoolTime(coolTime));
        }

        inventory.RemoveAt(index);
    }

    void ItemNull(int index)
    {
        inventory.slots[index].item = null;
    }
}
