using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 offsetOnPlay;
    [SerializeField] private Transform player;
    [SerializeField] float lerpOnPlay = 0.15f;

    private void LateUpdate()
    {
        MoveCamera();
    }
    private void MoveCamera()
    {

        transform.position = Vector3.Lerp(transform.position,
            player.position + offsetOnPlay,
            lerpOnPlay);

        transform.LookAt(player);
    }
}