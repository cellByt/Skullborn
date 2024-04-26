using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void Start()
    {
        instance = this;
        player = GetComponent<Player>();

        Cursor.visible = false;

        deathScreen.SetActive(false);
        pauseScreen.SetActive(false);

        deathButtons[0].onClick.AddListener(Restart);
        deathButtons[1].onClick.AddListener(Menu);

        pauseButtons[0].onClick.AddListener(Resume);
        pauseButtons[1].onClick.AddListener(Restart);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseScreen();
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
        if (!onPause && !ShopSytem.instance.shopIsOpen && !player.isDeath)
        {
            pauseScreen.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0f;
            onPause = true;
        }
        else
        {
            pauseScreen.SetActive(false);
            Cursor.visible = false;
            Time.timeScale = 1f;
            onPause = false;
        }

    }

    private void Resume()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        Cursor.visible = false;
        onPause = false;
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
}
