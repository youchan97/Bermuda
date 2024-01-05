 using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BearMotion : MonoBehaviour
{
    public AudioClip[] auClip;
    public float timeCount;

    public float result;
    int i;
    public int soundNum;

    GameObject gm;
    AdventureGameManager gmComponent;
    EndingManager emComponent;
    PlayerProperty playerProperty;
    public GameObject audioGameobj;

    public bool start;
    public BearState state;

    public enum BearState
    {
        deepSleep,
        ramSleep,
        halfEyes,
        WakeUp
    };
    
    void Start()
    {
        gm = GameObject.Find("GameManager");
        gmComponent = gm.GetComponent<AdventureGameManager>();

        gm = GameObject.Find("Player");
        playerProperty = gm.GetComponent<PlayerProperty>();

        result = Random.Range(4, 7);
    }

    void Update()
    {
        if (start == true)
        {
            if (state == BearState.deepSleep)
                DeepSleep(); 
            else if (state == BearState.ramSleep)
                RamSleep(); 
            else if (state == BearState.halfEyes)
                HalfEyes();
            else if (state == BearState.WakeUp)
                WakeUp();
        }
        else if(start == false)
        {
            Nothing();
        }

    }

    public void TimeValue(int i)
    {
        timeCount = Time.time + result + i;
    }//Ÿ�԰� ��Ǻ��� ���� �ٸ��� ����
    public void BeatState()
    {
        state = BearState.ramSleep;
    }//�ܺο��� �⺻ ��ô�� ������ ���� �Լ�

    void DeepSleep() //ǫ��
    {
        if (soundNum != 0) //����������� 1ȸ�� ����ǰ� üũ����
        {
            Setting(0);
            soundNum = 0;//�㿡�� ����ȵǰ�
        }
        if (gmComponent.keyCount >= result) //Űī��Ʈ Ȯ��
        {
            TimeValue(2);//���� ��ô�ӽ� �Ⱓ 2�ʴ� �÷��ֱ� ����
            gmComponent.keyCount = 0; //�����ش� �� ���³Ѿ���� Űī��Ʈ �ʱ�ȭ
            state = BearState.ramSleep;
        }
        else if (timeCount <= Time.time) //Ÿ�ӿ�����
        {
            TimeValue(0);
            gmComponent.keyCount = 0; //������ ���³Ѿ���� Űī��Ʈ �ʱ�ȭ
            state = BearState.deepSleep;
        }
    }
    public void RamSleep() //��ô��
    {
        if (soundNum != 1) //����������� 1ȸ�� ����ǰ� üũ����
        {
            Debug.Log("��ô��");
            Setting(1);
            soundNum = 1;
        }
        if (gmComponent.keyCount >= result)
        {
            TimeValue(0);
            gmComponent.keyCount = 0;
            state = BearState.halfEyes;
        }
        else if (timeCount <= Time.time)
        {
            TimeValue(0);
            gmComponent.keyCount = 0;
            state = BearState.deepSleep;
        }
    }
    void HalfEyes() //�Ǵ���
    {
        Debug.Log("�Ǵ���");
        audioGameobj.GetComponent<AudioSource>().Stop();
        soundNum = 4;//null�� ����� ����
        if (gmComponent.keyCount >= 2)
        {
            TimeValue(2);
            gmComponent.keyCount = 0;
            state = BearState.WakeUp;
        }
        else if (timeCount <= Time.time)
        {
            TimeValue(2);
            gmComponent.keyCount = 0;
            state = BearState.ramSleep;
        }
    }
    public void WakeUp() //����
    {
        gmComponent.keyCount = 0;
        soundNum = 4;//null�� ����� ����
        Debug.Log("���� ����");
        start = false;
        playerProperty.bearDie = true;

    }
    void Nothing()
    {
        gmComponent.keyCount = 0;
        audioGameobj.GetComponent<AudioSource>().Stop();
    }

    void OnCollisionEnter(Collision collision)//�ٷ��Ͼ�� ����
    {
        Debug.Log("���� �浹 : "+collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            WakeUp();
        }
    }

    void Setting(int i)
    {
        if (gmComponent.keyCount == 0)
        {
            audioGameobj.GetComponent<AudioSource>().clip = auClip[i];
            audioGameobj.GetComponent<AudioSource>().Play();
        }
    }
}
