using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScene : MonoBehaviour
{
    [SerializeField] private SettingsPopUp settingsPopUp;
    [SerializeField] private Text character;
    [SerializeField] private Text enemy;
    [SerializeField] private Scrollbar VidaPlayer;
    [SerializeField] private Scrollbar VidaEnemigo;
    private CharController player;
    private EnemigoController enemigo;
    private int MaxVidaPlayer;
    private int MaxVidaEnemigo;

    public void Start()
    {
        settingsPopUp.Close();
        character.text = PlayerPrefs.GetString("character");
        enemy.text = "Derrick";

    }

    public void Pause()
    {
        settingsPopUp.Open();
    }

    private void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharController>();
        }
        if(enemigo == null)
        {
            enemigo = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemigoController>();
        }
        if(player != null && enemigo != null)
        {
            VidaPlayer.size = player.life / player.MaxVida;
            VidaEnemigo.size = enemigo.Vida / enemigo.MaxVida;
        }
    }

}
