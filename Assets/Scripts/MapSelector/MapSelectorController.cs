using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelectorController : MonoBehaviour
{
    [SerializeField] private Button mapSelect;
    private string name;

    [SerializeField] private GameObject music;
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("BattleSound") != null) Destroy(GameObject.FindGameObjectWithTag("BattleSound"));
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1) Destroy(music);
        else DontDestroyOnLoad(music);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickAtras();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnClickSelection();
        }

        if (name == null) mapSelect.interactable = false;
        else mapSelect.interactable = true;
    }

    public void SetName(string map)
    {
        name = map;
    }

    public void OnClickSelection()
    {
        if(name != null)
        {
            PlayerPrefs.SetString("map", name);
            PlayerPrefs.SetString("WinPlayer1", "No");
            PlayerPrefs.SetString("WinPlayer2", "No");
            PlayerPrefs.SetString("WinEnemy1", "No");
            PlayerPrefs.SetString("WinEnemy2", "No");
            PlayerPrefs.SetString("PlayerVictory", "No");
            PlayerPrefs.SetString("EnemyVictory", "No");
            if (PlayerPrefs.GetString("Modo")=="Solo") SceneManager.LoadScene("GameScene");
            else if(PlayerPrefs.GetString("Modo")=="Versus") SceneManager.LoadScene("MultiplayerScene");
        }
    }

    public void OnClickAtras()
    {
        if (PlayerPrefs.GetString("Modo") == "Solo") SceneManager.LoadScene("SelectCharacter");
        else if (PlayerPrefs.GetString("Modo") == "Versus") SceneManager.LoadScene("SelectSecondCharacter");
        
    }
}
