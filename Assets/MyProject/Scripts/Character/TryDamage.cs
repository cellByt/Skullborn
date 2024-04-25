using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryDamage : MonoBehaviour
{
    private void TakeDamage()
    {
        Character _character = GetComponentInParent<Character>();
        GameObject _bloodEffect = GameObject.FindGameObjectWithTag("BloodEffect");

        var _targets = Physics2D.OverlapCircleAll(

            _character.posAtk.position,
            _character.attackRadius,
            _character.enemyLayer
            );

        foreach (var _target in _targets)
        {
            _target.GetComponent<Character>().TakeDamage(_character.attackDamage);
            GameObject _blood = Instantiate(_bloodEffect, _target.transform.position, Quaternion.identity);
            Destroy(_blood, 0.43f);
        }
    }
}
