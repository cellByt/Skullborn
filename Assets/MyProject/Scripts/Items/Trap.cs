using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float damageValue;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Player") || _other.CompareTag("Enemy"))
        {
            _other.GetComponent<Character>().Death();
        }
    }
}
