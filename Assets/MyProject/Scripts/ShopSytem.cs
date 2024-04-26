using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSytem : MonoBehaviour
{
    public static ShopSytem instance;

    [Header("Shop Variables")]
    public GameObject shopIndicator;
    public GameObject shopPanel;
    public TMP_Text healingText;
    [SerializeField] private Button[] shopButtons;
    public bool canOpenShop;
    public bool shopIsOpen;

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        shopIndicator.SetActive(false);
        shopPanel.SetActive(false);

        instance = this;

        shopButtons[0].onClick.AddListener(SellSkulls);
        shopButtons[1].onClick.AddListener(BuyHealingPot);
        shopButtons[2].onClick.AddListener(BuyDamage);

    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Player"))
        {
            shopIndicator.SetActive(true);
            canOpenShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.CompareTag("Player"))
        {
            shopIndicator.SetActive(false);
            canOpenShop = false;
        }
    }

    public void Shop()
    {
        if (!shopIsOpen)
        {
            shopPanel.SetActive(true);
            shopIndicator.SetActive(false);
            Cursor.visible = true;
            shopIsOpen = true;
        }
        else if (shopIsOpen)
        {
            shopPanel.SetActive(false);
            shopIndicator.SetActive(false);
            Cursor.visible = false;
            shopIsOpen = false;
        }
    }

    private void SellSkulls()
    {
        if (player.skulls > 0)
        {
            player.LostSkulls(1);
            player.money += 2;
            Debug.Log("Skull sell");
        }
    }

    private void BuyHealingPot()
    {
        if (player.money >= 16)
        {
            player.money = Mathf.Max(player.money - 16, 0);
            player.healingPots++;
            healingText.text = player.healingPots.ToString();
            Debug.Log("Buy healing");
        }
    }

    private void BuyDamage()
    {
        if (player.money >= 50)
        {
            player.money = Mathf.Max(player.money - 50, 0);
            player.attackDamage += 5;
            Debug.Log("Buy damage");
        }
    }
}
