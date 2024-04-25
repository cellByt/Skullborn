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
    private Rigidbody2D playerRB;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Application.isEditor) UpdatePlayerThings();
    }

    private void UpdatePlayerThings()
    {
        infoTexts[0].gameObject.SetActive(true);
        infoTexts[0].text = "X: " + player.position.x.ToShortString() + " Y: " + player.position.y.ToShortString();

        infoTexts[1].gameObject.SetActive(true);
        infoTexts[1].text = "Velocity_X: " + playerRB.velocity.x.ToShortString() + " Velocity_Y: " + playerRB.velocity.y.ToShortString();
    }
}
