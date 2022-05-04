using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float movementMultiplier = 10f, rotationLerpValue = 6f;
    private Rigidbody characterRb;
    private Animator characterAnimator;
    private Vector3 tempVelocity;
    private Vector3 lookPos;
    private Quaternion rotation;
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
        RotateCharacter();
    }
    private void UpdateTempVelocity()
    {
        tempVelocity = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical) * movementMultiplier;

        // if (joystick.Horizontal != 0.0f || joystick.Vertical != 0.0f)
        // {
        //     transform.LookAt(transform.position + characterRb.velocity);
        // }
    }
    private void UpdateCharacterVelocity()
    {
        characterRb.velocity = tempVelocity;

        if (characterRb.velocity != Vector3.zero & !characterAnimator.GetBool("IsWalking"))
        {
            characterAnimator.SetBool("IsWalking", true);
        }
        else if (characterRb.velocity == Vector3.zero & characterAnimator.GetBool("IsWalking"))
        {
            characterAnimator.SetBool("IsWalking", false);
        }
    }

    private void RotateCharacter()
    {
        lookPos = characterRb.velocity;
        lookPos.y = 0;
        rotation = Quaternion.LookRotation(lookPos);
        if (rotation.eulerAngles != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationLerpValue);
        }

    }

}
