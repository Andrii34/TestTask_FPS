using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttckState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;
    
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if (enemy.CanseePlaer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer+= Time.deltaTime; 
            enemy.transform.LookAt(enemy.Player.transform);
            if(shotTimer>enemy.fireRate)
            {
                Shoot();
            }
            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 2));
                moveTimer= 0;
            }
            enemy.LastKnowPos= enemy.Player.transform.position;
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                stateMachine.ChangesState(new SearchState());
            }
        }
    }
    public void Shoot()
    {
       enemy.Weapon.StartShoot();
    }

    
}
