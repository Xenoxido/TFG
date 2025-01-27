﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //para las escenas


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject music;
    [SerializeField] private GameObject modoPanel;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject control;
    [SerializeField] private GameObject credits;
    private bool modeSelect;
    private bool popUpOpen;
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("BattleSound") != null) Destroy(GameObject.FindGameObjectWithTag("BattleSound"));
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1) Destroy(music);
        else DontDestroyOnLoad(music);
    }
    // Start is called before the first frame update
    void Start()
    {
        modeSelect = false;
        popUpOpen = false;
        modoPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(popUpOpen);
        if (Input.GetKeyDown(KeyCode.Escape) && !modeSelect)
        {
            if (popUpOpen)
            {
                control.SetActive(false);
                credits.SetActive(false);
                popUpOpen = false;
            }
            else Application.Quit();
        }else if (Input.GetKeyDown(KeyCode.Escape) && modeSelect)
        {
            Close();
        }

        if (Input.GetKeyDown(KeyCode.Return) && !modeSelect)
        {
            Open();
        }else if(Input.GetKeyDown(KeyCode.Return) && modeSelect)
        {
            OnClickSolo();
        }

        
    }

    public void loaderScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Open()
    {
        modeSelect = true;
        modoPanel.SetActive(true);
        mainPanel.SetActive(false);

    }
    public void Close()
    {
        modeSelect = false;
        mainPanel.SetActive(true);
        modoPanel.SetActive(false);
    }

    public void OnClickSolo()
    {
        PlayerPrefs.SetString("Modo", "Solo");
        loaderScene("SelectCharacter");
    }
    public void OnClickVersus()
    {
        PlayerPrefs.SetString("Modo", "Versus");
        loaderScene("SelectCharacter");
    }
    public void OnOpenPopUp()
    {
        popUpOpen = true;
    }
    public void OnClosePopUp()
    {
        popUpOpen = false;
    }

}
