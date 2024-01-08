using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] items; //아이템을 담아둘 공간
    public Transform bag; // 슬롯의 위치를 잡기 위함
    public Slot[] slots; //인벤토리의 슬롯

    void Awake()
    {
        items = new Item[4]; //아이템의 공간 할당
        slots = bag.GetComponentsInChildren<Slot>(); //슬롯의 위치 설정
    }

    public void AddItem(Item _item) //인벤토리(UI)에 아이템 추가
    {
        for (int i = 0; i < 4; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].item = _item;
                slots[i].image.sprite = _item.itemImgage;
                return;
            }
        }
    }

    public void RemoveAt(int index) //인벤토리(UI)에 아이템 제거
    {
        slots[index].item = null;
        slots[index].image.sprite = null;
    }
}
