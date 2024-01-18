using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaseTrap : Maze.Trap
{
    MaseTrapStrategy maseTrapStrategy;

    private void Start()
    {
        maseTrapStrategy = new MaseTrapStrategy(this);
    }

    void Update()
    {
        maseTrapStrategy.TrapMove();
    }
}
