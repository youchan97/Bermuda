using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkTrap : TrapManager
{
	bool isDown; //If the wall starts down, if not you must modify to false
	public bool IsDown
	{
		get => isDown;
		set
		{
			isDown = value;
			vec = isDown ? 1 : -1;
        }
	}
	int vec;

	private float height; //Height of the platform
	private float posYDown; //Start position of the Y coord
	private bool isWaiting = false; //If the wall is waiting up or down
	private bool canChange = true; //If the wall is thinking if should go down or not

	void Awake()
	{
		IsDown = true;
        height = transform.localScale.y;
		if (IsDown)
			posYDown = transform.position.y;
		else
			posYDown = transform.position.y - height;
	}

	void Update()
	{
		if (IsDown)
		{
			TrapMove(vec);

        }
		else
		{
			if (!canChange)
				return;

			TrapMove(vec);

        }
	}
	void TrapMove(int vec)
	{
        if (transform.position.y > posYDown)
        {
            transform.position += vec * Vector3.up * Time.deltaTime * speed;
        }
        else if (!isWaiting)
            StartCoroutine(WaitToChange(0.25f));
    }
	//Function that wait before go down or up
	IEnumerator WaitToChange(float time)
	{
		isWaiting = true;
		yield return new WaitForSeconds(time);
		isWaiting = false;
		IsDown = !IsDown;

		StartCoroutine(Retry(1.5f));
	}

	//Function that checks every 1.25secs if can go down the wall
	IEnumerator Retry(float time)
	{
		canChange = false;
		yield return new WaitForSeconds(time);
		StartCoroutine(Retry(1.25f));
		canChange = true;
	}
}
