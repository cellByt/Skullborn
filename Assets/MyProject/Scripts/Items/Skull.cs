using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    private Player player;
    public int value;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.CompareTag("Player"))
        {
            player.GainSkulls(value);
            Destroy(gameObject);
        }
    }
}
