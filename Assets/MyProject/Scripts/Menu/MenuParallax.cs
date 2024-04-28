using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [SerializeField] private float offsetMultiplier;
    [SerializeField] private float smoothTime;

    private Vector2 initialPos;
    private Vector3 speed;

    private void Start()
    {
        initialPos = transform.position;
    }

    private void Update()
    {
        Vector2 _offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Vector3.SmoothDamp(transform.position, initialPos + (_offset * offsetMultiplier), ref speed, smoothTime);
    }
}
