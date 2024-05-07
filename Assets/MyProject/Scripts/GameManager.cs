using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Death Screen")]
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private Button[] deathButtons;

    [Header("Pause Screen")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Button[] pauseButtons;
    private bool onPause;

    private Player player;
    private Boss boss;

    [Header("Boss Collisions")]
    [SerializeField] private Collider2D[] collisions;

    [Header("Win Screen")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TMP_Text initialText;
    [SerializeField] private Button menu;
    public bool win;

    [SerializeField] private float sensitivity = 2.0f;

    private void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();

        Cursor.visible = false;

        deathScreen.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);

        deathButtons[0].onClick.AddListener(Restart); //Restart Button
        deathButtons[1].onClick.AddListener(Menu); //Menu Button

        pauseButtons[0].onClick.AddListener(Resume); //Resume Button
        pauseButtons[1].onClick.AddListener(Restart); //Restart Button

        menu.onClick.AddListener(Menu);
    }

    private void Update()
    {
        OnBossFight();

        if (boss.isDeath)
            WinScreen();
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7) && !player.isDeath && !ShopSytem.instance.shopIsOpen && !win)
        {
            PauseScreen();
            Cursor.visible = !Cursor.visible;
        }
    }

    #region Screens
    public void DeathScreen()
    {
        ShopSytem.instance.shopPanel.SetActive(false);
        pauseScreen.SetActive(false);
        deathScreen.SetActive(true);
        Cursor.visible = true;
    }

    private void PauseScreen()
    {
        if (!onPause)
        {
            pauseScreen.SetActive(true);
            ShopSytem.instance.shopPanel.SetActive(false);
            ShopSytem.instance.canOpenShop = false;
            Time.timeScale = 0f;
            onPause = true;
        }
        else
        {
            pauseScreen.GetComponent<Animator>().SetTrigger("Close");
            Time.timeScale = 1f;
            onPause = false;
        }

    }

    private void Resume()
    {
        pauseScreen.GetComponent<Animator>().SetTrigger("Close");
        Cursor.visible = false;
        Time.timeScale = 1f;
        onPause = false;
    }

    private void WinScreen()
    {
        win = true;

        initialText.gameObject.SetActive(true);
        winScreen.SetActive(true);
        deathScreen.SetActive(false);
        pauseScreen.SetActive(false);
        Cursor.visible = true;
    }

    private void Restart()
    {
        SceneManager.LoadScene("Phase_1");
        Time.timeScale = 1f;
    }

    private void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    #endregion

    private void OnBossFight()
    {
        if (boss.onFight)
        {
            foreach (var _collision in collisions)
            {
                _collision.isTrigger = false;
            }
        }
        else
        {
            foreach (var _collision in collisions)
            {
                _collision.isTrigger = true;
            }
        }
    }
}
