using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    public CharacterData characterData;
    [SerializeField] private Rigidbody characterRb;
    [SerializeField] private Animator characterAnimator;
    private Vector3 tempVelocity;
    private Vector3 lookPos;
    private Quaternion rotation;
    [SerializeField] private ParticleSystem characterWalkParticle;

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
        tempVelocity = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical) * characterData.movementMultiplier;

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

            characterWalkParticle.Play();
        }
        else if (characterRb.velocity == Vector3.zero & characterAnimator.GetBool("IsWalking"))
        {
            characterAnimator.SetBool("IsWalking", false);

            characterWalkParticle.Stop();
        }
    }

    private void RotateCharacter()
    {
        lookPos = characterRb.velocity;
        lookPos.y = 0;
        if (lookPos != Vector3.zero)
        {
            rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * characterData.rotationLerpValue);
        }

    }
}
