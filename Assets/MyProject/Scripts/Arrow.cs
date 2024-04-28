using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        Destroy(gameObject, 4f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.CompareTag("Player") && !player.rolling)
        {
            _other.GetComponent<Character>().TakeDamage(GameObject.FindGameObjectWithTag("Archery").GetComponent<Enemy>().attackDamage);
            Destroy(gameObject);
        }
        else if (_other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
