using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : MonoBehaviour, IUseable
{
    public Sprite itemImage;
    public float runningTime;

    public abstract void Use();
    public abstract IEnumerator ReturnCool(float time);
}
