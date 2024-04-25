using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Necromancer : MonoBehaviour
{
    [SerializeField] UnityEvent necromancerAttack;

    public void Attack()
    {
        necromancerAttack.Invoke();
    }
}
