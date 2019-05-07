using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //para las escenas


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject music;
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("BattleSound") != null) Destroy(GameObject.FindGameObjectWithTag("BattleSound"));
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1) Destroy(music);
        else DontDestroyOnLoad(music);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loaderScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
