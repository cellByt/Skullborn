using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeController : MonoBehaviour
{

    [Header("Life Variables")]
    [SerializeField] protected float currentLife;
    [SerializeField] protected float maxLife;
    [SerializeField] private Slider lifeSlider;
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

        if (lifeSlider != null)
        {
            lifeSlider.maxValue = maxLife;
            lifeSlider.value = currentLife;
        }
    }

    protected virtual void Death()
    {
        isDeath = true;
    }

    public void TakeDamage(float _damage)
    {
        if (isDeath || !canTakeDamage) return;

        Debug.Log(_damage);
        currentLife = Mathf.Max(currentLife - _damage, 0f);
        if (lifeSlider != null) StartCoroutine(UpdateLifeSlider());
        if (haveDisplayDMG) FloatingDamage(_damage);

        if (currentLife == 0) Death();
    }

    private IEnumerator UpdateLifeSlider()
    {
        float _preLife = lifeSlider.value;
        float _duration = 0.5f;
        float _time = 0f;

        while (_time < _duration)
        {
            _time += Time.deltaTime;
            float _lerpSpeed = Mathf.Clamp01(_time / _duration);
            lifeSlider.value = Mathf.Lerp(_preLife, currentLife, _lerpSpeed);

            yield return null;
        }

        lifeSlider.value = currentLife;
    }

    private void FloatingDamage(float _damage)
    {
        damageTXT.text = _damage.ToString();

        Vector3 _newPos = transform.position + textPos;
        GameObject _text = Instantiate(floatingDmg, _newPos, quaternion.identity);

        Destroy(_text, 0.45f);
    }

}
