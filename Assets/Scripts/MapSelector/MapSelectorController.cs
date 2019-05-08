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
            if(PlayerPrefs.GetString("Modo")=="Solo") SceneManager.LoadScene("GameScene");
            else if(PlayerPrefs.GetString("Modo")=="Versus") SceneManager.LoadScene("MultiplayerScene");
        }
    }

    public void OnClickAtras()
    {
        if (PlayerPrefs.GetString("Modo") == "Solo") SceneManager.LoadScene("SelectCharacter");
        else if (PlayerPrefs.GetString("Modo") == "Versus") SceneManager.LoadScene("SelectSecondCharacter");
        
    }
}
