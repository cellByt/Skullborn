using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : LifeController
{

    protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;

    [Header("Movement Variables")]
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected bool canJump;
    [SerializeField] protected bool canMove;
    [SerializeField] protected float groundRadius;
    [SerializeField] protected Transform posPe;
    [SerializeField] protected LayerMask groundLayer;
    public bool facingLeft;
    protected Vector2 direction;

    [Header("Attack Variables")]
    public LayerMask enemyLayer;
    public float attackRadius;
    [SerializeField] protected float attackDPS;
    public float attackDamage;
    protected float initialDamage;
    protected float nextAttack;
    [SerializeField] protected float attackIndex;
    public Transform posAtk;
    public enum AttackType
    {
        Melee,
        Ranged
    }
    public AttackType atkType;

    [Header("SoundEffects")]
    [SerializeField] protected AudioClip[] soundEffect;
    protected AudioSource audioS;
    public bool takeDamage;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioS = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        base.Start();

        initialDamage = attackDamage;
        canMove = true;
    }

    protected virtual void Update()
    {
        Anim();

        if (facingLeft && transform.localScale.x < 0 || !facingLeft && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (isDeath) return;

        Move();

        if (canJump) Jump();
    }

    #region Movement

    protected void Move()
    {
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    protected bool OnGround()
    {
        bool checkGround = Physics2D.OverlapCircle(
            posPe.position,
            groundRadius,
            groundLayer
            );

        return checkGround;
    }

    protected void Jump()
    {
        canJump = false;

        Vector2 _velocity = rb.velocity;
        _velocity.y = 0;
        rb.velocity = _velocity;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        if (audioS != null) PlaySound(1);
    }

    protected void Flip()
    {
        if ((transform.localScale.x > 0 && rb.velocity.x < -0.2f) || (transform.localScale.x < 0 && rb.velocity.x > 0.2f))
        {
            facingLeft = !facingLeft;

            Vector3 _localScale = transform.localScale;
            _localScale.x *= -1f;
            transform.localScale = _localScale;
        }
    }

    #endregion

    #region Attack

    protected void Attack()
    {
        if (Time.timeSinceLevelLoad < nextAttack || isDeath) return;

        nextAttack = Time.timeSinceLevelLoad + 1 / attackDPS;
        if (gameObject.tag == "Player") attackIndex++;

        switch (atkType)
        {
            case AttackType.Melee:
                MeleeAttack();
                direction.x = 0f;
                canMove = false;

                if (audioS != null) PlaySound(0);
                break;

            case AttackType.Ranged:
                RangeAttack();
                direction.x = 0f;

                if (audioS != null) PlaySound(0);
                break;
        }
    }

    protected void MeleeAttack()
    {
        anim.SetFloat("AttackIndex", attackIndex);
        anim.SetTrigger("Attack");
    }

    protected virtual void RangeAttack()
    {
        anim.SetTrigger("Attack");
    }

    #endregion

    #region SoundEffects

    protected void PlaySound(int _audioIndex)
    {
        audioS.clip = soundEffect[_audioIndex];
        audioS.Play();
    }

    #endregion

    #region Animation

    protected virtual void Anim()
    {
        #region Move
        anim.SetBool("OnGround", OnGround());

        int _x = 1;

        if (direction.x != 0) anim.SetFloat("Speed_X", _x);
        else anim.SetFloat("Speed_X", 0);

        float _y = 0f;

        if (rb.velocity.y > 0.2f) _y = 1f;
        else if (rb.velocity.y < -0.2f) _y = -1f;

        anim.SetFloat("Speed_Y", _y);

        #endregion

        anim.SetBool("Death", isDeath);
    }

    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);

        if (audioS != null) PlaySound(2);
        if (gameObject.tag != "Boss" && gameObject.tag != "Archery") anim.SetTrigger("Hit");
    }

    #endregion

    public override void Death()
    {
        base.Death();

        rb.velocity = new Vector2(0f, 0f);
    }
}
