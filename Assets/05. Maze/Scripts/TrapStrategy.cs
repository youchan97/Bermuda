using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class TrapStrategy
{
    protected Maze.Trap trap;
    public TrapStrategy(Maze.Trap trap)
    {
        this.trap = trap;
    }

    public abstract void TrapMove();

}

public class MaseTrapStrategy : TrapStrategy
{
    float limit = 75f;
    public MaseTrapStrategy(Maze.Trap trap) : base(trap)
    {
        trap.speed = 2f;
    }

    public override void TrapMove()
    {
        //반복적인 운동을 하기 위한 주기 함수인 싸인 함수 사용
        float angle = limit * Mathf.Sin(Time.time * trap.speed);
        trap.transform.localRotation = Quaternion.Euler(0, 0, angle + 180);
    }
}

public class ForkTrapStrategy : TrapStrategy
{
    bool isDown = true; //트랩의 위치 조건

    float height; //트랩 객체의 높이(길이)
    float posYDown; //트랩의 시작 y점
    bool isWaiting = false; //위아래로 동작 바뀌기 전 잠시 기다리는 조건
    bool canChange = true; //트랩의 동작이 바뀌는 조건
    public ForkTrapStrategy(Maze.Trap trap) : base(trap)
    {
        height = trap.transform.localScale.y;
        if (isDown)
            posYDown = trap.transform.position.y;
        else
            posYDown = trap.transform.position.y - height;
    }

    public override void TrapMove()
    {
        if (isDown)
        {
            if (trap.transform.position.y < posYDown + height)
            {
                trap.transform.position += Vector3.up * Time.deltaTime * trap.speed;
            }
            else if (!isWaiting)
                trap.StartCoroutine(WaitToChange(0.25f));
        }
        else
        {
            if (!canChange)
                return;

            if (trap.transform.position.y > posYDown)
            {
                trap.transform.position -= Vector3.up * Time.deltaTime * trap.speed;
            }
            else if (!isWaiting)
                trap.StartCoroutine(WaitToChange(0.25f));
        }
    }
    //동작이 바뀌기 전 잠시 정지
    IEnumerator WaitToChange(float time)
    {
        isWaiting = true;
        yield return new WaitForSeconds(time);
        isWaiting = false;
        isDown = !isDown;

        trap.StartCoroutine(Retry(1.5f));
    }

    //움직이는 시간 후 동작 반대
    IEnumerator Retry(float time)
    {
        canChange = false;
        yield return new WaitForSeconds(time);
        trap.StartCoroutine(Retry(1.25f));
        canChange = true;
    }
}
