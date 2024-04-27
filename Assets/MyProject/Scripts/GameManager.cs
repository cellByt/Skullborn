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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

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
        if (Input.GetKeyDown(KeyCode.Escape) && !player.isDeath && !ShopSytem.instance.shopIsOpen)
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
        if (!onPause)
        {
            pauseScreen.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0f;
            onPause = true;
        }
        else
        {
            pauseScreen.GetComponent<Animator>().SetTrigger("Close");
            Cursor.visible = false;
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
