using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searthTimer;
    private float moveTime;
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.LastKnowPos);
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if(enemy.CanseePlaer())
        {
            stateMachine.ChangesState(new AttckState());
        }
        if (enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
        {
            searthTimer+=Time.deltaTime;
            moveTime += Time.deltaTime;
            if(moveTime> Random.Range(2,5))
            {
                enemy.Agent.SetDestination(enemy.transform.position+(Random.insideUnitSphere*10));
                moveTime= 0;
            }
            Debug.Log(searthTimer.ToString());
            if(searthTimer> 5) 
            {
                stateMachine.ChangesState(new PatrolState());
            }

        }
       
    }
}
