using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCMent : MonoBehaviour
{
    public TextMeshProUGUI tempText;
    public int mouseClickCount;
    public int mouseClickCount2 = 0;
    public float timeCount;

    public GameObject boxKey;

    GameObject gm;
    PlayerProperty playerProperty;
    PlayerData playerData;

    void Start()
    {
        mouseClickCount = 0;

        gm = GameObject.Find("Player");
        playerProperty = gm.GetComponent<PlayerProperty>();
        playerData = FindObjectOfType<PlayerData>();
    }

    void Update()
    {

    }

    public void NPClickMent()
    {
        if (playerProperty.bearFoodQuest == false && playerData.adventureClear ==false)
        {
            mouseClickCount++;
            if (mouseClickCount == 1)
            {
                tempText.text = "��...�ΰ��ΰ�? �̰� �� �ݰ�����!(NPC�� ��������)";
            }
            else if (mouseClickCount == 2)
            {
                tempText.text = "�׷��� �Ϳ��ٴ� ������ ���������ٷ�? �̷��Ƶ� �Ѷ��� ��û�� �����翴�ٱ�!(NPC�� ��������)";
            }
            else if (mouseClickCount == 3)
            {
                tempText.text = "���͵鿡�� ���ؼ� ������ �̷� ���� ������ �Ǿ�����...(NPC�� ��������)";
            }
            else if (mouseClickCount == 4)
            {
                tempText.text = "������ ������ �� �����ټ� �����Ű���. ������ ��Ź�� �ϳ��־�.(NPC�� ��������)";
            }
            else if (mouseClickCount == 5)
            {
                tempText.text = "������ �Դ� ������ �׸�� ���Կ� ����̴� ���Ա�����...(NPC�� ��������)";
            }
            else if (mouseClickCount == 6)
            {
                tempText.text = "�ξ��� ������ ���� �ڰ��ִµ� �� ������ �̱� �� ���� �ʰ� ����� �� ���־�?(����Ʈ�� �����Ϸ��� NPC�� ��������)";
            }
            else if (mouseClickCount == 7)
            {
                tempText.text = " ";
            }
        }
        else if (playerProperty.bearFoodQuest == true && playerData.adventureClear == false)
        {
            mouseClickCount2++;
            if (mouseClickCount2 == 1)
            {
                tempText.text = "�;ƾ�! ��û ���־�! ��ƿ��! ��ø�!(NPC�� ��������)";
            }
            else if (mouseClickCount2 == 2)
            {
                tempText.text = "��! �� ���踦 ������. ���� �ʿ��� �ʿ��� �� ����. ���߿� ���������� �ʿ��ϸ� �ٽ� ã����!(������ �������� NPC�� ��������)";
            }
            else if (mouseClickCount2 == 3)
            {
                tempText.text = " ";
                Instantiate(boxKey, new Vector3(0, 7.3f, -4.5f), Quaternion.identity);
            }
        }
        else
        {
            tempText.text = " ";
        }
    }
}
