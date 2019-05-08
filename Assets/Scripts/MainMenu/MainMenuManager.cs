using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //para las escenas


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject music;
    [SerializeField] private GameObject modoPanel;
    [SerializeField] private GameObject mainPanel;
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("BattleSound") != null) Destroy(GameObject.FindGameObjectWithTag("BattleSound"));
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1) Destroy(music);
        else DontDestroyOnLoad(music);
    }
    // Start is called before the first frame update
    void Start()
    {
        modoPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loaderScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Open()
    {
        modoPanel.SetActive(true);
        mainPanel.SetActive(false);

    }
    public void Close()
    {
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

}
