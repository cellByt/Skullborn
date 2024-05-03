using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCave : MonoBehaviour
{
    [SerializeField] private GameObject cave;
    private bool activeLerp = false;

    private void Start()
    {
        cave.GetComponentInChildren<SpriteRenderer>().color = Color.black;
    }

    private void Update()
    {
        if (activeLerp)
            cave.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.black, Color.white, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.tag == "Player")
            activeLerp = true;
    }
}
