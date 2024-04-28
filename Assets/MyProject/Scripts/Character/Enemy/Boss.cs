using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss Variables")]
    [SerializeField] private Transform rangePosAtk;
    [SerializeField] private float rangeAtkDPS;
    [SerializeField] private float defaultAtkDps;

    private AttackType currentState = AttackType.Melee;

    protected override void Start()
    {
        base.Start();

        defaultAtkDps = attackDPS;
    }

    protected override void IA()
    {
        if ((player.gameObject.transform.position.x - transform.position.x) <= distAgro)
        {
            attackDPS = rangeAtkDPS;

            currentState = AttackType.Ranged;

            attackIndex = 2;
            Debug.Log("Range");
            Attack();
        }
        else
        {
            attackDPS = defaultAtkDps;

            currentState = AttackType.Melee;

            Debug.Log("Melee");
            attackIndex = 1;
            Attack();
        }
    }

    public void LaunchSword()
    {
        GameObject _sword = Instantiate(arrowPrefab, rangePosAtk.position, Quaternion.identity);
        _sword.GetComponent<Rigidbody2D>().velocity = new Vector2(-arrowSpeed, 0f);

        Destroy(_sword, 3f);
    }

}
