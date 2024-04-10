using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class ShowThings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] infoTexts;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Application.isEditor) UpdatePlayerThings();
    }

    private void UpdatePlayerThings()
    {
        infoTexts[0].gameObject.SetActive(true);
        infoTexts[0].text = "X: " + player.position.x.ToShortString() + " Y: " + player.position.y.ToShortString();
    }
}
