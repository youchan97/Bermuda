using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] items; //�������� ��Ƶ� ����
    public Transform bag; // ������ ��ġ�� ��� ����
    public Slot[] slots; //�κ��丮�� ����

    void Awake()
    {
        items = new Item[4]; //�������� ���� �Ҵ�
        slots = bag.GetComponentsInChildren<Slot>(); //������ ��ġ ����
    }

    public void AddItem(Item _item) //�κ��丮(UI)�� ������ �߰�
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

    public void RemoveAt(int index) //�κ��丮(UI)�� ������ ����
    {
        slots[index].item = null;
        slots[index].image.sprite = null;
    }
}
