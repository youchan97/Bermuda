using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaseTrap : Maze.Trap
{
    MaseTrapStrategy maseTrapStrategy; //진자 운동 장애물에 대한 최대 각도

    private void Start()
    {
        maseTrapStrategy = new MaseTrapStrategy(this);
    }

    void Update()
    {
        maseTrapStrategy.TrapMove();
    }
}
