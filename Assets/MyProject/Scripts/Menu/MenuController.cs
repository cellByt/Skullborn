using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button[] menuButtons;
    [SerializeField] private GameObject[] menuScreens;
    [SerializeField] private Slider loadingBar;

    private void Start()
    {
        menuButtons[0].onClick.AddListener(Exit); //Exit Button

        menuScreens[0].SetActive(true); //Menu Screen
        menuScreens[1].SetActive(false); //Loading Screen
    }

    public void LoadLevel(string _levelToLoad)
    {
        menuScreens[0].SetActive(false);
        menuScreens[1].SetActive(true);

        StartCoroutine(LoadLevelSync(_levelToLoad));
    }

    private IEnumerator LoadLevelSync(string _levelToLoad)
    {
        AsyncOperation _loadOperation = SceneManager.LoadSceneAsync(_levelToLoad);

        while (!_loadOperation.isDone)
        {
            float _progressValue = Mathf.Clamp01(_loadOperation.progress / 0.9f);
            loadingBar.value = Mathf.Lerp(loadingBar.value, _progressValue, 0.1f);

            yield return null;
        }

    }

    private void Exit()
    {
        Application.Quit();
        Debug.Log("EXIT");
    }
}
