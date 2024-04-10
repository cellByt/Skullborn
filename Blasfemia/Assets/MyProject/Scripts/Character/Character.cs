using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;

    [Header("Movement Variables")]
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected bool canJump;
    [SerializeField] protected Transform posPe;
    [SerializeField] protected LayerMask groundLayer;
    protected float groundRadius = 0.03f;
    protected bool facingLeft;
    protected Vector2 direction;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Anim();
    }

    protected virtual void FixedUpdate()
    {
        Move();

        if(canJump) Jump();
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
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        canJump = false;
    }

    protected void Flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(Vector2.up * 180f);
    }

    #endregion

    #region Animation

    protected virtual void Anim()
    {
        #region Move
        int _x = 1;

        if (direction.x != 0) anim.SetFloat("Speed_X", _x);
        else anim.SetFloat("Speed_X", 0);

        float _y = 1f;

        if (rb.velocity.y > 0.2f) _y = 1f;
        else if (rb.velocity.y < -0.2f) _y = -1f;

        anim.SetFloat("Speed_Y", _y);

        #endregion
    }

    #endregion
}
