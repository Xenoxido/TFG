using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScene : MonoBehaviour
{
    [SerializeField] private SettingsPopUp settingsPopUp;
    [SerializeField] private Text character;

    public void Start()
    {
        character.text = PlayerPrefs.GetString("character");
    }

    public void Pause()
    {
        settingsPopUp.Open();
    }

}
