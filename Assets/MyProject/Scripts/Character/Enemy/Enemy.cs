using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    [Header("IA Variables")]
    [SerializeField] protected float distAgro;
    [SerializeField] private float iaTime;
    protected Player player;
    private Vector2 destiny;
    [SerializeField] private float patrolTime;
    [SerializeField] private float patrolRadius;
    private float initialPos_X;
    private float nextPatrol;

    [Header("Range Attack Variables")]
    public GameObject arrowPrefab;
    public float arrowSpeed;

    [Header("Necromancer Variable")]
    [SerializeField] private GameObject enemyPreFab;

    [Header("Drop Skulls")]
    [SerializeField] private GameObject skull;
    [SerializeField] private int skullsQuant;

    protected override void Awake()
    {
        base.Awake();

        initialPos_X = transform.position.x;
    }

    protected override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        InvokeRepeating("IA", 0f, iaTime);
    }

    #region IA
    protected virtual void IA()
    {
        if (isDeath) return;

        if (!player || player.isDeath)
        {
            Patrol();
            return;
        }

        if (DistanceToPlayer() <= distAgro && player.gameObject.transform.position.y <= transform.position.y)
        {
            if (DistanceToPlayer() <= attackRadius)
            {
                direction.x = 0f;
                FlipToPlayer();
                Attack();
            }
            else FollowPlayer();
        }
        else
        {
            Patrol();
        }
    }

    protected float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.gameObject.transform.position);
    }

    private void Patrol()
    {
        if (Time.time >= nextPatrol)
        {
            nextPatrol = Time.time + patrolTime;
            destiny = transform.position;
            destiny.x = initialPos_X + Random.Range(-patrolRadius, patrolRadius);

            if (destiny.x < transform.position.x)
            {
                direction.x = -1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (destiny.x > transform.position.x)
            {
                direction.x = 1;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
                direction.x = 0;
        }
        else if (Vector2.Distance(destiny, transform.position) < 0.5f) direction.x = 0f;
    }

    private void FollowPlayer()
    {
        if (DistanceToPlayer() < attackRadius)
            direction.x = 0f;
        else if (player.gameObject.transform.position.x > transform.position.x)
        {
            direction.x = 1f;
        }
        else if (player.gameObject.transform.position.x < transform.position.x)
        {
            direction.x = -1f;
        }
    }

    protected void FlipToPlayer()
    {
        float _distPlayer = player.transform.position.x - transform.position.x;

        if (_distPlayer > 0.4f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingLeft = false;
        }
        else if (_distPlayer < -0.4f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingLeft = true;
        }
    }

    #endregion

    #region Archery

    public void Arrow()
    {
        GameObject _arrow = Instantiate(arrowPrefab, posAtk.position, Quaternion.identity);

        if (facingLeft)
        {
            _arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-arrowSpeed, 0f);
            _arrow.transform.Rotate(Vector2.up * 180f);
        }
        else
            _arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed, 0f);
    }

    #endregion

    #region Necromancer
    public void SummonGoblin()
    {
        GameObject _clone = Instantiate(enemyPreFab, posAtk.position, Quaternion.identity);

        if (facingLeft)
        {
            _clone.GetComponent<Character>().facingLeft = true;
            _clone.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
        }
    }

    public override void Death()
    {
        base.Death();

        if (skull != null) DropSkulls();

        if (gameObject.tag == "Necromancer") Invoke("DestroyObject", 0.84f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    #endregion

    #region ItemsDrop
    private void DropSkulls()
    {
        Instantiate(skull, transform.position, Quaternion.identity);
        Skull _skull = GameObject.FindGameObjectWithTag("Skull").GetComponent<Skull>();
        _skull.value = skullsQuant;
    }
    #endregion
}
