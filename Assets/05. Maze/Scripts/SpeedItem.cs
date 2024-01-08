using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : Item
{
    [SerializeField]
    private float addSpeed;
    public override IEnumerator ReturnCool(float time)
    {
        yield return new WaitForSeconds(time);
        GameController.instance.player.moveSpeed -= addSpeed;
    }

    public override void Use()
    {
        GameController.instance.player.moveSpeed += addSpeed;
        StartCoroutine(ReturnCool(runningTime));
    }
}
