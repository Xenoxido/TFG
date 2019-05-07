﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject music;
    [SerializeField] private AudioClip fight;

    [SerializeField] private Material[] skyboxes = new Material[6];
    private string nameMap;

    private string namechar;
    private string namenemy;
    [SerializeField] private GameObject ethan;
    [SerializeField] private GameObject atenea;
    [SerializeField] private GameObject eve;

    [SerializeField] private GameObject derrick;

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
            case "Castle":
                RenderSettings.skybox = skyboxes[1];
                break;
            case "DarkCity":
                RenderSettings.skybox = skyboxes[2];
                break;
            case "Beach":
                RenderSettings.skybox = skyboxes[3];
                break;
            case "AnotherPlanet":
                RenderSettings.skybox = skyboxes[4];
                break;
            case "Aurora":
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
                Instantiate(ethan).gameObject.tag = "Player";
                break;
            case "Atenea":
                Instantiate(atenea).gameObject.tag = "Player";
                break;
            case "Eve":
                Instantiate(eve).gameObject.tag = "Player";
                break;
            default:
                break;
        }

        Instantiate(derrick).gameObject.tag = "Enemy";

    }
}
