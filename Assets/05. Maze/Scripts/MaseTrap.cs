using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaseTrap : TrapManager 
{
    public float limit = 75f; //���� � ��ֹ��� ���� �ִ� ����

    private void Start()
    {
        speed = 2f; //TrapManager�� �ִ� speed���� �ʱ�ȭ
    }

    void Update()
    {
        //�ݺ����� ��� �ϱ� ���� �ֱ� �Լ��� ���� �Լ� ���
        float angle = limit * Mathf.Sin(Time.time * speed); 
        transform.localRotation = Quaternion.Euler(0, 0, angle + 180);
    }
}
