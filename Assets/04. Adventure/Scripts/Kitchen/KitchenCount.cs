using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class KitchenCount : MonoBehaviour
{
    GameObject gm;
    BearMotion bearMotion;

    public TextMeshProUGUI tempText;
    IEnumerator coroutine; //��ž�ڷ�ƾ�� ����ϱ� ���� �������ִ� ��
    void Start()
    {
        gm = FindObjectOfType<BearMotion>().gameObject;
        bearMotion = gm.GetComponent<BearMotion>();

        coroutine = OnSecondCheckCo(); //��ŸƮ�� �ߵȴ��ؼ� ��ž�� �ߵ�������.
                                       //������ IEnumerator �޼��带 �����Ҷ����� �������� �ٲ�� ����
                                       //OnSecondCheckCo �޼��带 ������ ��ȯ���� StartCoroutine �޼��忡 ����
                                       //�� OnSecondCheckCo ���� ���� �������� �ٲ�ԵǾ� ��ž�� �ص� �ߴܵ��� ����
                                       //���� ���� ���� �� �Ҵ����� ��
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            StartCoroutine(coroutine);

            bearMotion.start = true;
            bearMotion.soundNum = 4;//�ʱ�ȭ ���� �ȿ����� ���¿� ���� �˾Ƽ� �ٲ��
            Debug.Log("������������");
            bearMotion.TimeValue(2);
            bearMotion.BeatState();
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            StopCoroutine(coroutine); //�̰��� �Ͻ�����
            coroutine = OnSecondCheckCo(); //�ڷ�ƾ �ð� �ʱ�ȭ
            Debug.Log("������������");
            bearMotion.start = false;
            bearMotion.soundNum = 4;
            bearMotion.BeatState();
        }
    }

    IEnumerator OnSecondCheckCo()
    {
        int time = 180;
        while (time > 0)
        {
            //������ ī��Ʈ
            tempText.text = time + " ��";
            yield return new WaitForSeconds(1);
            time--;
        }
        bearMotion.WakeUp();
    }
}