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
    public bool rolling;
    [SerializeField] private bool canRoll;

    [Header("Combo Variables")]
    [SerializeField] private float nextCombo;
    [SerializeField] private float comboRate;
    [SerializeField] private bool isInCombo;

    [Header("Skulls Variables")]
    public int skulls;
    [SerializeField] TMP_Text skullsQuant;
    [SerializeField] TMP_Text skullsText;
    [SerializeField] Vector3 offset;

    [Header("Change Status Variables")]
    public int money;
    public int healingPots;

    [Header("Fast Fall")]
    [SerializeField] private float fallGravity;
    private float defaultGravity;

    protected override void Start()
    {
        base.Start();

        canRoll = true;
        canMove = true;

        defaultGravity = rb.gravityScale;

        skullsText.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        UpdateHearts();
        PlayerInputs();
        //PlayWalkingSound();

        comboRate += Time.deltaTime;

        if (direction.x < 0 && !facingLeft && !isDeath || direction.x > 0 && facingLeft && !isDeath)
        {
            Flip();
            CameraFollow.instance.offset.x *= -1f;
        }

        if (rb.velocity.y < 0 && !isDeath)
            rb.gravityScale = fallGravity;
        else if (!isDeath)
            rb.gravityScale = defaultGravity;
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

        if (Input.GetKeyDown(KeyCode.F) && !isDeath && currentLife != maxLife && healingPots > 0 && OnGround())
        {
            canMove = false;
            direction.x = 0f;

            healingPots = Mathf.Max(healingPots - 1, 0);

            PlaySound(3);
            anim.SetTrigger("UseItem");
            Invoke("ReturnToMove", 0.38f);

            GainLife(2);
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

    /*
    private void PlayWalkingSound()
    {
        if (rb.velocity.x != 0)
        {
            if (!audioS.isPlaying)
            {
                PlaySound(3);
            }
        }
        else
        {
            audioS.Stop();
        }
    }
    */
    #endregion

    private void ComboSystem()
    {
        if (isInCombo)
        {
            if (attackIndex > 3 || comboRate >= nextCombo)
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

        skullsText.gameObject.SetActive(true);

        skullsText.color = Color.green;
        skullsQuant.text = skulls.ToString();
        skullsText.text = "+ " + _skulls.ToString();

        Invoke("DisableText", 0.5f);
    }

    public void LostSkulls(int _skulls)
    {
        skulls = Mathf.Max(skulls - _skulls, 0);

        skullsText.gameObject.SetActive(true);

        skullsText.color = Color.red;
        skullsQuant.text = skulls.ToString();
        skullsText.text = "- " + _skulls.ToString();

        Invoke("DisableText", 0.5f);
    }

    private void DisableText()
    {
        skullsText.gameObject.SetActive(false);
    }

    #endregion

    #region Death
    public override void Death()
    {
        base.Death();

        GameManager.instance.DeathScreen();
    }

    #endregion
}
