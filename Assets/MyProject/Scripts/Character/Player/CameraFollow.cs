using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public static CameraFollow instance;

    [Header("Smooth Follow Variables")]
    public Vector3 offset;
    [SerializeField] private float smoothSpeed;
    private Transform playerPos;

    private void Start()
    {
        instance = this;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 _newPos = playerPos.position + offset;
        Vector3 _smoothPos = Vector3.Lerp(transform.position, _newPos, smoothSpeed);
        transform.position = _smoothPos;
    }
}
