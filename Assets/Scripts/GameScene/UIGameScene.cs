﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameScene : MonoBehaviour
{
    [SerializeField] private SettingsPopUp settingsPopUp;
    [SerializeField] private ControlsPopUp controlsPopUp;
    [SerializeField] private Text character;
    [SerializeField] private Text enemy;
    [SerializeField] private Scrollbar VidaPlayer;
    [SerializeField] private Scrollbar VidaEnemigo;
    [SerializeField] private WinPopUp WinPanel;

    //Imagenes de las victorias
    [SerializeField] private Image WinPlayer1;
    [SerializeField] private Image WinPlayer2;
    [SerializeField] private Image WinEnemy1;
    [SerializeField] private Image WinEnemy2;


    private CharController player;
    private FirstCharController first;
    private SecondCharController second;
    private EnemigoController enemigo;
    private int MaxVidaPlayer;
    private int MaxVidaEnemigo;

    private float time;

    private bool pause;
    private bool controls;

    public void Start()
    {
        time = Time.timeScale;
        pause = false;
        controls = false;
        Debug.Log(PlayerPrefs.GetString("Modo"));
        settingsPopUp.Close();
        character.text = PlayerPrefs.GetString("character");
        if (PlayerPrefs.GetString("Modo") == "Solo") enemy.text = "Derrick";
        else if (PlayerPrefs.GetString("Modo") == "Versus") enemy.text = "Second " + PlayerPrefs.GetString("secondCharacter");
        WinPlayer1.gameObject.SetActive(PlayerPrefs.GetString("WinPlayer1")=="Yes");
        WinPlayer2.gameObject.SetActive(PlayerPrefs.GetString("WinPlayer2") == "Yes");
        WinEnemy1.gameObject.SetActive(PlayerPrefs.GetString("WinEnemy1") == "Yes");
        WinEnemy2.gameObject.SetActive(PlayerPrefs.GetString("WinEnemy2") == "Yes");
        WinPanel.Close();
    }

    public void Pause()
    {
        pause = true;
        settingsPopUp.Open();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (pause && !controls)
            {
                Time.timeScale = time;
                settingsPopUp.Close();
                pause = false;
            }
            else if (!pause)
            {
                Time.timeScale = time;
                SceneManager.LoadScene("MapSelector");
            }
            if (controls)
            {
                controlsPopUp.Close();
                controls = false;
            }
            
        }
        if (PlayerPrefs.GetString("Modo") == "Solo")
        {
            if(player == null) player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharController>();
            if(enemigo == null) enemigo = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemigoController>();
            if(player != null && enemigo != null)
            {
                VidaPlayer.size = player.life / player.MaxVida;
                VidaEnemigo.size = enemigo.Vida / enemigo.MaxVida;
            }
        }
        else if (PlayerPrefs.GetString("Modo") == "Versus")
        {
            if(first == null) first = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstCharController>();
            if(second == null) second = GameObject.FindGameObjectWithTag("Enemy").GetComponent<SecondCharController>();
            if(first != null && second != null)
            {
                VidaPlayer.size = first.life / first.MaxVida;
                VidaEnemigo.size = second.life / second.MaxVida;
            }
        }   

        if (PlayerPrefs.GetString("EnemyVictory") == "Yes") StartCoroutine(EndGame(enemy.text));
        else if (PlayerPrefs.GetString("PlayerVictory") == "Yes") StartCoroutine(EndGame(character.text));

    }

    public IEnumerator EndGame(string winner)
    {
        yield return new WaitForSeconds(5);
        WinPanel.Open(winner);
    }

    public void OnBackPause()
    {
        pause = false;
    }
    public void OnControls()
    {
        controls = true;
    }
    public void OnBackControls()
    {
        controls = false;
    }

}
