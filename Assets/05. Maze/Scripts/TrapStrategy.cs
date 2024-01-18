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
        //�ݺ����� ��� �ϱ� ���� �ֱ� �Լ��� ���� �Լ� ���
        float angle = limit * Mathf.Sin(Time.time * trap.speed);
        trap.transform.localRotation = Quaternion.Euler(0, 0, angle + 180);
    }
}

public class ForkTrapStrategy : TrapStrategy
{
    bool isDown = true; //Ʈ���� ��ġ ����

    float height; //Ʈ�� ��ü�� ����(����)
    float posYDown; //Ʈ���� ���� y��
    bool isWaiting = false; //���Ʒ��� ���� �ٲ�� �� ��� ��ٸ��� ����
    bool canChange = true; //Ʈ���� ������ �ٲ�� ����
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
    //������ �ٲ�� �� ��� ����
    IEnumerator WaitToChange(float time)
    {
        isWaiting = true;
        yield return new WaitForSeconds(time);
        isWaiting = false;
        isDown = !isDown;

        trap.StartCoroutine(Retry(1.5f));
    }

    //�����̴� �ð� �� ���� �ݴ�
    IEnumerator Retry(float time)
    {
        canChange = false;
        yield return new WaitForSeconds(time);
        trap.StartCoroutine(Retry(1.25f));
        canChange = true;
    }
}
