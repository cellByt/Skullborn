using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    [Header("Boss Variables")]
    [SerializeField] private Transform rangePosAtk;
    [SerializeField] private float minRangeAtkDPS;
    [SerializeField] private float maxRangeAtkDPS;
    [SerializeField] private float defaultAtkDps;
    [SerializeField] private int hitsToPause;
    [SerializeField] private int currentHits;
    [SerializeField] private float bossPauseTime;
    public bool isPaused;
    public bool onFight;

    [Header("Life Bar")]
    [SerializeField] private GameObject lifeBar;
    [SerializeField] private Slider lifeBarSlider;
    [SerializeField] private Slider smoothLifeBar;
    [SerializeField] private float smoothSpeed;

    private AttackType currentState = AttackType.Melee;

    protected override void Start()
    {
        base.Start();

        isPaused = true;
        defaultAtkDps = attackDPS;

        lifeBarSlider.maxValue = maxLife;
        smoothLifeBar.maxValue = maxLife;
    }

    protected override void Update()
    {
        base.Update();

        UpdateLifeBar();
    }

    #region IA
    protected override void IA()
    {
        if (!player || player.isDeath || isPaused) return;

        if ((player.gameObject.transform.position.x - transform.position.x) <= distAgro)
        {
            attackDPS = Random.Range(minRangeAtkDPS, maxRangeAtkDPS);

            currentState = AttackType.Ranged;

            attackIndex = 2;

            Attack();
        }
        else
        {
            attackDPS = defaultAtkDps;

            currentState = AttackType.Melee;

            attackIndex = 1;
            Attack();
        }
    }

    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);

        currentHits++;

        if (currentHits >= hitsToPause)
        {
            isPaused = true;
            Invoke("StopPause", bossPauseTime);
        }
    }

    private void StopPause()
    {
        isPaused = false;
        currentHits = 0;
    }
    #endregion

    #region Attacks
    public void LaunchSword()
    {
        GameObject _sword = Instantiate(arrowPrefab, rangePosAtk.position, Quaternion.identity);
        _sword.GetComponent<Rigidbody2D>().velocity = new Vector2(-arrowSpeed, 0f);

        Destroy(_sword, 3f);
    }

    #endregion

    #region LifeBar
    private void UpdateLifeBar()
    {
        if (!onFight) lifeBar.SetActive(false);
        else lifeBar.SetActive(true);

        if (lifeBarSlider.value != currentLife)
        {
            lifeBarSlider.value = currentLife;
        }

        if (smoothLifeBar.value != currentLife)
        {
            smoothLifeBar.value = Mathf.Lerp(smoothLifeBar.value, currentLife, smoothSpeed);
        }
    }

    #endregion

    #region Death
    public override void Death()
    {
        base.Death();

        onFight = false;
    }

    #endregion

    #region Anim
    protected override void Anim()
    {
        base.Anim();

        anim.SetBool("Paused", isPaused);
    }

    #endregion
}
