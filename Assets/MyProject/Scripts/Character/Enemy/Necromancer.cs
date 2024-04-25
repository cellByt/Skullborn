using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : Enemy
{
    [Header("Necromancer Variable")]
    [SerializeField] private GameObject enemyPreFab;

    protected override void RangeAttack()
    {
        base.RangeAttack();

        Instantiate(enemyPreFab, posAtk.position, Quaternion.identity);
    }

    protected override void Death()
    {
        base.Death();

        Invoke("DestroyObject", 0.84f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
