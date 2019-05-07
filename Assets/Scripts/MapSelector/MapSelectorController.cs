using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectorController : MonoBehaviour
{
    private string name;

    [SerializeField] private GameObject music;
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("BattleSound") != null) Destroy(GameObject.FindGameObjectWithTag("BattleSound"));
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1) Destroy(music);
        else DontDestroyOnLoad(music);
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
            SceneManager.LoadScene("GameScene");
        }
    }

    public void OnClickAtras()
    {
        SceneManager.LoadScene("SelectCharacter");
    }
}
