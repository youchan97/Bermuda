using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
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
            Debug.Log((index+1) + "�� ����Ű�� ��� �� �� �ִ� �������� �����ϴ�.");
        }
    }
    private void UseItem(int index) //�������� ���Ǿ��� �� �� ���� �̹��� �ҷ����� ����
    {
        //inventory.slots[index].Use();

        if (inventory.slots[index].item.name == "SpeedUp")
        {
            float time = 5f;
            player.moveSpeed += speedUpValue;
            inventory.slots[index].item = null;
            StartCoroutine(SpeedCoolTime(time));
        }
        else if (inventory.slots[index].item.name == "Invisible")
        {
            float time = 3f;
            player.isInvisible = true;
            inventory.slots[index].item = null;
            StartCoroutine(InvisibleCoolTime(time));
        }
        else if (inventory.slots[index].item.name == "RouteSpray")
        {
            float time = 5f;
            DirectionUi.SetActive(true);
            inventory.slots[index].item = null;
            StartCoroutine(SprayCoolTime(time));
        }

        inventory.RemoveAt(index);
    }
   
}
