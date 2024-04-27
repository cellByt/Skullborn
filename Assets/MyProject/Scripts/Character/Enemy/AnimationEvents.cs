using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] UnityEvent rangeAttack;
    [SerializeField] UnityEvent necromancerAttack;

    public void Attack()
    {
        rangeAttack.Invoke();
        necromancerAttack.Invoke();
    }
}
