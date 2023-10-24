
using UnityEngine;

internal class randomMoveState : BaseState
{
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 50));
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if (enemy.CanseePlaer())
        {
            stateMachine.ChangesState(new AttckState());
        }
        if (enemy.Agent.remainingDistance < 0.2f)
        {

            enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 50));
        }
        
    }
}