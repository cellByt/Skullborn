using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageArrow : MonoBehaviour
{
    [SerializeField] UnityEvent rangeAttack;

    public void Attack()
    {
        rangeAttack.Invoke();
    }
}
