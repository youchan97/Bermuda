using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class RouteSprayItem : Item
{
    public override IEnumerator ReturnCool(float time)
    {
        yield return new WaitForSeconds(time);
        GameController.instance.DirectionUi.SetActive(false);
    }

    public override void Use()
    {
        GameController.instance.DirectionUi.SetActive(true); // 올바른 길 UI 생성
        StartCoroutine(ReturnCool(runningTime));
    }
}
