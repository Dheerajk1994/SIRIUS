using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform playerToFollow;
    public float cameraSmoothSpeed;
    Vector3 desiredLocation;
    Vector3 velocity = Vector3.zero;
    public Vector3 offset;

    public void Start()
    {
        this.transform.position = (playerToFollow.position) + offset;
    }

    public void FixedUpdate()
    {
        desiredLocation = playerToFollow.position + offset;
        this.transform.position = Vector3.SmoothDamp(transform.position, desiredLocation, ref velocity, cameraSmoothSpeed);
    }

    public void SetFocus(Transform focus)
    {
        playerToFollow = focus;
    }

}
