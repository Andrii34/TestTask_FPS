using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour, ITakingDamage
{
    [SerializeField] private float healf =100;
    private Enemy enemyScript;
    private StateMachine stateMachine;
    private Animator deadAnimator;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform animationAnchor;
    private float targetAngle = 80.0f;
    private float targetYPosition = 0.7f;
    private float duration = 1.0f;

    public void TakeDamage(float damage)
    {
        healf-=damage;
        if (healf <= 0)
        {
            
            

          
            weapon.Drop();
            StartCoroutine(DeadAnim());
            DisableAllComponentsExceptColliderAndMesh();
            enemyScript.enabled= false;
            stateMachine.enabled= false;
            

        }

    }

    private 
    void Start()
    {
        enemyScript = GetComponent<Enemy>();
        stateMachine = GetComponent<StateMachine>();
        deadAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private IEnumerator DeadAnim()
    {
        float startTime = Time.time;
        float elapsedTime = 0;

        Quaternion startRotation = transform.rotation;
        Vector3 startPosition = transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime = Time.time - startTime;

            
            float t = Mathf.Clamp01(elapsedTime / duration);
            Quaternion newRotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, targetAngle, t));
            transform.rotation = newRotation;

            
            Vector3 newPosition = new Vector3(startPosition.x, Mathf.Lerp(startPosition.y, targetYPosition, t), startPosition.z);
            transform.position = newPosition;

            yield return null;
        }

        
    }
    private void DisableAllComponentsExceptColliderAndMesh()
    {
       
        Collider collider = GetComponent<Collider>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

       
        Component[] components = GetComponents<Component>();
        foreach (Component component in components)
        {
            if (component != collider && component != meshRenderer)
            {
                if (component is Behaviour) 
                {
                    ((Behaviour)component).enabled = false;
                }
                else if (component is Renderer) 
                {
                    ((Renderer)component).enabled = false;
                }
            }
        }
    }
}
