using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StsteMachine : MonoBehaviour
{
    public BaseState activeState;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeState(BaseState newState)
    {
        if(activeState != null)
        {
            activeState.Exit();
        }
        activeState= newState;

        if(activeState != null )
        {

        }
    }
}
