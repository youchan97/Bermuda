using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkTrap : Maze.Trap
{
    ForkTrapStrategy forkTrapStrategy;

    private void Start()
    {
        forkTrapStrategy = new ForkTrapStrategy(this);
    }

    void Update()
    {
        forkTrapStrategy.TrapMove();
    }
}
