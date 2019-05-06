using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPopUp : MonoBehaviour
{
    [SerializeField] private Text winText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(string winner)
    {
        gameObject.SetActive(true);
        winText.text = winner + " has won!";
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene("SelectCharacter");
    }
}
