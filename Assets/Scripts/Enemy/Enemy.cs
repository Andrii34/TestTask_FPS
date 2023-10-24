using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    [SerializeField] private StartState startState;
    private string curentState;
    [SerializeField] private Weapon weapon;
    [SerializeField ] private float eyeHeight = 0.7f;
   
    private GameObject player;
    private Vector3 lastKnowPos;
    public Weapon Weapon { get  => weapon;  }
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get =>player; }
    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }

    [SerializeField]
    public Path path;
    [SerializeField] private EnemyParameters enemyParameters;
    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;
    private float sightDistanse;
    private float fieldOfviem;

    enum StartState
    {
        PatrolState,
        randomMoveState
    }
    void Start()
    {
        sightDistanse = enemyParameters.sightDistance;
        fieldOfviem = enemyParameters.fieldOfView;

        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        StartState initialState = startState;
        weapon.DamagePerShot = enemyParameters.damage;
        switch (initialState)
        {
            case StartState.PatrolState:
                if (path != null)
                {
                    stateMachine.Initalise(new PatrolState());
                }
                else
                {
                    stateMachine.Initalise(new randomMoveState());
                }
                break;
            case StartState.randomMoveState:
                stateMachine.Initalise(new randomMoveState());
                break;
        };
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CanseePlaer();
        curentState= stateMachine.activeState.ToString();

    }
    public bool CanseePlaer()
    {
        if(player!= null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistanse)
            {

                Vector3 targetDirection = player.transform.position - transform.position-(Vector3.up*eyeHeight);
                float angelToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angelToPlayer >= -fieldOfviem && angelToPlayer <= fieldOfviem)
                {
                    Ray ray = new Ray(transform.position+(Vector3.up*eyeHeight),targetDirection);
                    RaycastHit hitInfo= new RaycastHit();
                    if(Physics.Raycast(ray, out hitInfo,sightDistanse)) 
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin,ray.direction*sightDistanse);
                            return true;
                        }
                    }
                }
            }  
        }
        return false;
        
    }
}
