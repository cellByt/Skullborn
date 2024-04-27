using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowSecret : MonoBehaviour
{
    [SerializeField] private TMP_Text secretText;

    private void Start()
    {
        secretText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.CompareTag("Player"))
        {
            secretText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.gameObject.CompareTag("Player"))
        {
            secretText.gameObject.GetComponent<Animator>().SetTrigger("Close");
        }
    }
}
