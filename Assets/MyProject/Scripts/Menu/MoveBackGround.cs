using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float initialPos;
    [SerializeField] private float newPos;
    [SerializeField] private float maxPos;

    private void Start()
    {
        initialPos = transform.position.x;
    }

    private void Update()
    {
        MoveBG();
    }

    private void MoveBG()
    {
        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);

        if (transform.position.x < initialPos - maxPos)
        {

        }
    }
}
