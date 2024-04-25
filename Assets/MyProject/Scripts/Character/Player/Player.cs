using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Character
{
    [Header("Roll Variables")]
    [SerializeField] private float rollForce;
    [SerializeField] private float rollTime;
    [SerializeField] private bool rolling;

    [Header("Combo Variables")]
    [SerializeField] private float nextCombo;
    [SerializeField] private float comboRate;
    [SerializeField] private bool isInCombo;

    [Header("Skulls Variables")]
    [SerializeField] private int skulls;
    [SerializeField] GameObject skullsPreFab;
    [SerializeField] TMP_Text skullsText;
    [SerializeField] Vector3 offset;

    protected override void Update()
    {
        base.Update();

        PlayerInputs();
        comboRate += Time.deltaTime;
    }

    private void PlayerInputs()
    {
        if (rolling) return;

        float input_x = Input.GetAxisRaw("Horizontal");
        direction.x = input_x;

        if (Input.GetButtonDown("Jump") && OnGround()) canJump = true;
        if (Input.GetButtonDown("Fire1") && OnGround())
        {
            isInCombo = true;
            Attack();
            ComboSystem();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && OnGround()) StartCoroutine(Roll());
    }

    #region Movement
    private IEnumerator Roll()
    {
        anim.SetTrigger("Roll");
        rolling = true;
        canTakeDamage = false;

        if (facingLeft)
            direction.x = -rollForce;
        else
            direction.x = rollForce;

        yield return new WaitForSeconds(rollTime);

        direction.x = 0f;
        rolling = false;
        canTakeDamage = true;
    }

    private void ComboSystem()
    {
        if (isInCombo)
        {
            if (attackIndex > 2 || comboRate >= nextCombo)
            {
                isInCombo = false;
                comboRate = 0f;
                attackIndex = 0;
            }
            else Attack();
        }
    }

    #endregion

    public void GainSkulls(int _skulls)
    {
        skulls += _skulls;
        skullsText.text = "+ " + _skulls.ToString();

        Vector3 _newPos = transform.position + offset;
        GameObject _text = Instantiate(skullsPreFab, _newPos , Quaternion.identity);
        Destroy(_text.gameObject, 0.3f);
    }

    #region Death
    protected override void Death()
    {
        base.Death();

        GameManager.instance.DeathScreen();
    }

    #endregion
}
