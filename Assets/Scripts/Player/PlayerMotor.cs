using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool chrouching;
    private bool lerpCrouch;
    private bool sprinting;
    
    const float GRAVITY = -9.8f;
    [SerializeField] private  float crouchTimer = 1f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 3f;
    private PlayerStamina playerStamina;
    [SerializeField] private Weapon weapon;
    

    void Start()
    {
        controller= GetComponent<CharacterController>();
        playerStamina = GetComponent<PlayerStamina>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (sprinting)
        {
            playerStamina.stamina -= 2f * Time.deltaTime;
        }
        isGrounded= controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer+= Time.time;
            float p = crouchTimer/ 1;
            p*=p;
            if(chrouching)
            {
                
                controller.height= Mathf.Lerp(controller.height,1,p);
            }
            else
            {
                controller.height=Mathf.Lerp(controller.height,2, p);
            }

            if (p > 1)
            {
                 lerpCrouch= false;
                 crouchTimer= 0f;
            }
        }
            
    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection)*speed*Time.deltaTime);
        playerVelocity.y += GRAVITY * Time.deltaTime;
        if(isGrounded&&playerVelocity.x <0) 
        {
            playerVelocity.x = -2;
        }
        controller.Move(playerVelocity*Time.deltaTime);
    }
    public void Jump()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * GRAVITY);
        }
    }
    public void Chrouch()
    {
        Debug.Log("Crouch");
        chrouching = !chrouching;
        crouchTimer = 0;
        lerpCrouch=true;
    }
    public void Sprint()
    {
        if(playerStamina.stamina >= 5)
        {
            speed = 9f;
            playerStamina.stamina -=playerStamina.staminaDepletionRate * Time.deltaTime;       

        }
        else
        {
           speed= 5;
        }
    }
    public void StopSprint()
    {
        speed= 5;
    }

    public void Attack( )
    {
        
        weapon.StartShoot();
    }
}
