using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCanAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.CompareTag("Player") && !GetComponentInParent<Boss>().isDeath)
        {
            GetComponentInParent<Boss>().isPaused = false;
            GetComponentInParent<Boss>().onFight = true;
        }
    }
}
