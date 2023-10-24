using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int wayponIndex;
    public float waitTimer;
   
    public override void Enter()
    {
       
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        PatrolCycle();
        if (enemy.CanseePlaer())
        {
            stateMachine.ChangesState(new AttckState());
        }
    }
    public void PatrolCycle()
    {
      
        if (enemy.Agent.remainingDistance < 0.2f)

        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 0.3)
            {


                if (wayponIndex < enemy.path.waypoints.Count - 1)
                {
                    wayponIndex++;
                }
                else
                {
                    wayponIndex = 0;
                }
                enemy.Agent.SetDestination(enemy.path.waypoints[wayponIndex].position);
                waitTimer= 0;
            }
        } 
    }
}
