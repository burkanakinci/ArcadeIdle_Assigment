using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float movementMultiplier = 10f;
    private Rigidbody characterRb;
    private Animator characterAnimator;
    private Vector3 tempVelocity;
    private void Start()
    {
        characterRb = GetComponent<Rigidbody>();
        characterAnimator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        UpdateTempVelocity();
    }
    private void FixedUpdate()
    {
        UpdateCharacterVelocity();
    }
    private void UpdateTempVelocity()
    {
        tempVelocity = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical)*movementMultiplier;

        if(joystick.Horizontal!=0.0f||joystick.Vertical!=0.0f)
        {
            transform.LookAt(transform.position + characterRb.velocity);
        }
    }
    private void UpdateCharacterVelocity()
    {
        characterRb.velocity =  tempVelocity;

        if (characterRb.velocity != Vector3.zero & !characterAnimator.GetBool("IsWalking"))
        {
            characterAnimator.SetBool("IsWalking", true);
        }
        else if (characterRb.velocity == Vector3.zero & characterAnimator.GetBool("IsWalking"))
        {
            characterAnimator.SetBool("IsWalking", false);
        }
    }

}
