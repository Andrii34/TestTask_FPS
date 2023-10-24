using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
  
    protected override void Interact()
    {
        
        Vector3 randomForceDirection = Random.onUnitSphere;
        rb.AddForce(randomForceDirection* 5f, ForceMode.Impulse);
    }
}
