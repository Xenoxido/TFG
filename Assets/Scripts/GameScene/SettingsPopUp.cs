using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPopUp : MonoBehaviour
{
    private float time;

    private void Awake()
    {
        time = 1;
    }
    public void Open()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = time;
    }

    public void goTitle()
    {
        Time.timeScale = time;
        SceneManager.LoadScene("Main Menu");
    }

    public void goSelect()
    {
        Time.timeScale = time;
        SceneManager.LoadScene("SelectCharacter");
    }
}
