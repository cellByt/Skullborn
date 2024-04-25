using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.CompareTag("Player"))
        {
            _other.GetComponent<Character>().TakeDamage(GameObject.FindGameObjectWithTag("Archery").GetComponent<Enemy>().attackDamage);
            Destroy(gameObject);
        }
    }
}
