using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] popUps;
    [SerializeField] private int popUpIndex;

    private void Update()
    {
        if (PlayerPrefs.GetInt("PassedTutorial") == 0) PopUpUpdate();
    }

    private void PopUpUpdate()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
                popUps[i].SetActive(true);
            else
                popUps[i].SetActive(false);
        }

        if (popUpIndex == 0) // Movement Tutorial
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
                popUpIndex++;
        }
        else if (popUpIndex == 1) // Jump Tutorial
        {
            if (Input.GetButtonDown("Jump"))
                popUpIndex++;
        }
        else if (popUpIndex == 2) // Healing Pot Tutorial
        {
            if (Input.GetKeyDown(KeyCode.F))
                popUpIndex++;
        }
        else if (popUpIndex == 3) // Combat Tutorial
        {
            if (Input.GetButtonDown("Fire1"))
                popUpIndex++;
        }
        else if (popUpIndex == 4) // Roll Tutorial
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                popUpIndex++;
            }
        }
        else
        {
            PlayerPrefs.SetInt("PassedTutorial", 1);
        }
    }
}