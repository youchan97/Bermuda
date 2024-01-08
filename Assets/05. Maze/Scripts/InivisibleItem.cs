using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InivisibleItem : Item
{
    public override IEnumerator ReturnCool(float time)
    {
        GameController.instance.invisibleUI.SetActive(true);
        yield return new WaitForSeconds(runningTime);
        GameController.instance.invisibleUI.SetActive(false);
        GameController.instance.player.isInvisible = false;
    }

    public override void Use()
    {
        GameController.instance.player.isInvisible = true; // �÷��̾��� ���� ���� ����
        StartCoroutine(ReturnCool(runningTime));
    }
  
}
