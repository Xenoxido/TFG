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
    [SerializeField] private WinPopUp WinPanel;

    //Imagenes de las victorias
    [SerializeField] private Image WinPlayer1;
    [SerializeField] private Image WinPlayer2;
    [SerializeField] private Image WinEnemy1;
    [SerializeField] private Image WinEnemy2;

    private CharController player;
    private EnemigoController enemigo;
    private int MaxVidaPlayer;
    private int MaxVidaEnemigo;

    public void Start()
    {
        settingsPopUp.Close();
        character.text = PlayerPrefs.GetString("character");
        enemy.text = "Derrick";
        WinPlayer1.gameObject.SetActive(PlayerPrefs.GetString("WinPlayer1")=="Yes");
        WinPlayer2.gameObject.SetActive(PlayerPrefs.GetString("WinPlayer2") == "Yes");
        WinEnemy1.gameObject.SetActive(PlayerPrefs.GetString("WinEnemy1") == "Yes");
        WinEnemy2.gameObject.SetActive(PlayerPrefs.GetString("WinEnemy2") == "Yes");
        WinPanel.Close();
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

        if (PlayerPrefs.GetString("EnemyVictory") == "Yes") StartCoroutine(EndGame(enemy.text));
        if (PlayerPrefs.GetString("PlayerVictory") == "Yes") StartCoroutine(EndGame(character.text));

    }

    public IEnumerator EndGame(string winner)
    {
        yield return new WaitForSeconds(5);
        WinPanel.Open(winner);
    }

}
