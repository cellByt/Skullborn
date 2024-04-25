using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button[] menuButtons;

    private void Start()
    {
        menuButtons[0].onClick.AddListener(Play);
        menuButtons[1].onClick.AddListener(Exit);
    }

    private void Play()
    {
        SceneManager.LoadScene("Phase_1");
    }

    private void Exit()
    {
        Application.Quit();
        Debug.Log("EXIT");
    }
}
