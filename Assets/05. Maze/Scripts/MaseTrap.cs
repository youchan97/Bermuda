using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaseTrap : TrapManager 
{
    public float limit = 75f; //진자 운동 장애물에 대한 최대 각도

    private void Start()
    {
        speed = 2f; //TrapManager에 있는 speed변수 초기화
    }

    void Update()
    {
        //반복적인 운동을 하기 위한 주기 함수인 싸인 함수 사용
        float angle = limit * Mathf.Sin(Time.time * speed); 
        transform.localRotation = Quaternion.Euler(0, 0, angle + 180);
    }
}
