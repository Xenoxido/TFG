using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsPopUp : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private float time;

    private void Awake()
    {
        time = 1;
    }
    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volume");
    }
    private void Update()
    {
        AudioListener.volume = slider.value;
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
    }
    public void Open()
    {
        Time.timeScale = 0;
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().Pause();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>().Play();
        Time.timeScale = time;
    }

    public void goTitle()
    {
        Time.timeScale = time;
        SceneManager.LoadScene("Main Menu");
    }

    public void goSelect()
    {
        Time.timeScale = time;
        SceneManager.LoadScene("SelectCharacter");
    }
}
