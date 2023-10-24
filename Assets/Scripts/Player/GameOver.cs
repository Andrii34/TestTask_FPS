using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private PlayerHealf playerHealf;
    
    void Start()
    {
        playerHealf= GetComponent<PlayerHealf>();
    }

    
    void Update()
    {
        if (playerHealf.Healh<=0)
        {
            GameManager.instance.GameOver();
            Cursor.visible = true; 
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
