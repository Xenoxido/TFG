using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPopUp : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void goTitle()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void goSelect()
    {
        SceneManager.LoadScene("SelectCharacter");
    }
}
