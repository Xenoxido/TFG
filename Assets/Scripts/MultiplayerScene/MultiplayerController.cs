using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MultiplayerController : MonoBehaviour
{
    [SerializeField] private GameObject music;
    [SerializeField] private AudioClip fight;

    [SerializeField] private Material[] skyboxes = new Material[6];
    private string nameMap;

    private string namechar;
    private string namenemy;
    [SerializeField] private GameObject ethan1;
    [SerializeField] private GameObject atenea1;
    [SerializeField] private GameObject eve1;
    [SerializeField] private GameObject ethan2;
    [SerializeField] private GameObject atenea2;
    [SerializeField] private GameObject eve2;

    private void Awake()
    {
        Destroy(GameObject.FindGameObjectWithTag("Music"));
        if (GameObject.FindGameObjectsWithTag("BattleSound").Length > 1) Destroy(music);
        else DontDestroyOnLoad(music);
    }
    // Start is called before the first frame update
    void Start()
    {
        music.GetComponent<AudioSource>().PlayOneShot(fight);
        //Gestion de mapas
        nameMap = PlayerPrefs.GetString("map");
        switch (nameMap)
        {
            case "BlueFreeze":
                RenderSettings.skybox = skyboxes[0];
                break;
            case "MegaSun":
                RenderSettings.skybox = skyboxes[1];
                break;
            case "DarkCity":
                RenderSettings.skybox = skyboxes[2];
                break;
            case "Highlands":
                RenderSettings.skybox = skyboxes[3];
                break;
            case "UnearthlyRed":
                RenderSettings.skybox = skyboxes[4];
                break;
            case "Stratosphere":
                RenderSettings.skybox = skyboxes[5];
                break;
            default:
                break;
        }

        AudioListener.volume = PlayerPrefs.GetFloat("volume");//Control de volumen al inicio de partida
        //Gestion de personajes
        namechar = PlayerPrefs.GetString("character");
        switch (namechar)
        {
            case "Ethan":
                Instantiate(ethan1).gameObject.tag = "Player";
                break;
            case "Atenea":
                Instantiate(atenea1).gameObject.tag = "Player";
                break;
            case "Eve":
                Instantiate(eve1).gameObject.tag = "Player";
                break;
            default:
                break;
        }
        namenemy = PlayerPrefs.GetString("secondCharacter");
        switch (namenemy)
        {
            case "Ethan":
                Instantiate(ethan2).gameObject.tag = "Enemy";
                break;
            case "Atenea":
                Instantiate(atenea2).gameObject.tag = "Enemy";
                break;
            case "Eve":
                Instantiate(eve2).gameObject.tag = "Enemy";
                break;
            default:
                break;
        }

    }
}
