using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputControl : MonoBehaviour
{
    float horizontalMoveAmount = 0f;

    CharacterControllerManager controller;

    void Start()
    {
        controller = GetComponent<CharacterControllerManager>();
    }

    void Update()
    {
        horizontalMoveAmount = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
             controller?.Jump();
        }
        
    }

    void FixedUpdate()
    {
        var _amount = horizontalMoveAmount * Time.fixedDeltaTime;     //controller.Move(_amount);
        controller?.Move(_amount);
    }
}
