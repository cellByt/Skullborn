using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Character
{

    [Header("Roll Variables")]
    [SerializeField] private float rollForce;
    [SerializeField] private float rollTime;
    [SerializeField] private float nextRollTime;
    [SerializeField] private bool rolling;
    [SerializeField] private bool canRoll;

    [Header("Combo Variables")]
    [SerializeField] private float nextCombo;
    [SerializeField] private float comboRate;
    [SerializeField] private bool isInCombo;

    [Header("Skulls Variables")]
    public int skulls;
    [SerializeField] GameObject skullsPreFab;
    [SerializeField] TMP_Text skullsText;
    [SerializeField] Vector3 offset;

    [Header("Change Status Variables")]
    public int money;
    public int healingPots;

    protected override void Start()
    {
        base.Start();

        canRoll = true;
        canMove = true;
    }

    protected override void Update()
    {
        base.Update();

        UpdateHearts();
        PlayerInputs();
        comboRate += Time.deltaTime;
    }

    private void PlayerInputs()
    {
        if (rolling || !canMove) return;

        float input_x = Input.GetAxisRaw("Horizontal");
        direction.x = input_x;

        if (Input.GetButtonDown("Jump") && OnGround()) canJump = true;

        if (Input.GetButtonDown("Fire1") && OnGround() && !ShopSytem.instance.shopIsOpen)
        {
            isInCombo = true;

            Attack();
            ComboSystem();


            Invoke("ReturnToMove", 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && OnGround() && canRoll) StartCoroutine(Roll());

        if (Input.GetKeyDown(KeyCode.E) && ShopSytem.instance.canOpenShop) ShopSytem.instance.Shop();

        if (Input.GetKeyDown(KeyCode.F))
        {
            healingPots = Mathf.Max(healingPots--, 0);

            if (healingPots == 0) return;

            GainLife(100);
            ShopSytem.instance.healingText.text = healingPots.ToString();
        }
    }

    #region Movement
    private IEnumerator Roll()
    {
        anim.SetTrigger("Roll");
        rolling = true;
        canRoll = false;
        canTakeDamage = false;

        if (facingLeft)
            direction.x = -rollForce;
        else
            direction.x = rollForce;

        yield return new WaitForSeconds(rollTime);

        direction.x = 0f;
        canTakeDamage = true;
        rolling = false;

        yield return new WaitForSeconds(nextRollTime);
        canRoll = true;
    }

    private void ReturnToMove()
    {
        canMove = true;
    }

    #endregion

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

    #region SkullsSystem
    public void GainSkulls(int _skulls)
    {
        skulls += _skulls;
        skullsText.text = "+ " + _skulls.ToString();

        Vector3 _newPos = transform.position + offset;
        GameObject _text = Instantiate(skullsPreFab, _newPos , Quaternion.identity);
        Destroy(_text.gameObject, 0.3f);
    }

    public void LostSkulls(int _skulls)
    {
        skulls = Mathf.Max(skulls - _skulls, 0);

        skullsText.text = "- " + _skulls.ToString();

        Vector3 _newPos = transform.position + offset;
        GameObject _text = Instantiate(skullsPreFab, _newPos, Quaternion.identity);
        Destroy(_text.gameObject, 0.3f);
    }

    #endregion

    #region Death
    protected override void Death()
    {
        base.Death();

        GameManager.instance.DeathScreen();
    }

    #endregion
}
