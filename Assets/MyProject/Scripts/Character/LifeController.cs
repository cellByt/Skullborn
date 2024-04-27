using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class LifeController : MonoBehaviour
{

    [Header("Life Variables")]
    [SerializeField] protected float currentLife;
    [SerializeField] protected float maxLife;
    [SerializeField] private Animator heartAnim;
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] protected bool canTakeDamage;
    public bool isDeath;

    [Header("Display Damage")]
    [SerializeField] private GameObject floatingDmg;
    [SerializeField] private TMP_Text damageTXT;
    [SerializeField] private Vector3 textPos;
    [SerializeField] private bool haveDisplayDMG;

    protected virtual void Start()
    {
        canTakeDamage = true;
        currentLife = maxLife;

        heartAnim = GameObject.FindGameObjectWithTag("Heart").GetComponent<Animator>();
    }

    public virtual void Death()
    {
        isDeath = true;
    }

    #region Lost/Gain Life
    public virtual void TakeDamage(float _damage)
    {
        if (isDeath || !canTakeDamage) return;

        currentLife = Mathf.Max(currentLife - _damage, 0f);

        if (gameObject.tag == "Player") heartAnim.SetTrigger("Damage");

        if (haveDisplayDMG) FloatingDamage(_damage);

        if (currentLife == 0) Death();
    }

    public void GainLife(float _life)
    {
        if (isDeath) return;

        currentLife = Mathf.Min(currentLife + _life, maxLife);
        if (gameObject.tag == "Player") heartAnim.SetTrigger("Heal");

    }

    #endregion

    protected void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLife)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < maxLife)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void FloatingDamage(float _damage)
    {
        damageTXT.text = _damage.ToString();

        Vector3 _newPos = transform.position + textPos;
        GameObject _text = Instantiate(floatingDmg, _newPos, quaternion.identity);

        Destroy(_text, 0.45f);
    }

}
