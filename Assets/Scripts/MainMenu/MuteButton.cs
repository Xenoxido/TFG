using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MuteButton : MonoBehaviour
{
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;
    public bool volumenOn;
    // Start is called before the first frame update
    void Start()
    {
        volumenOn = true;
        if (AudioListener.pause)
        {
            volumenOn = false;
            GetComponent<Image>().sprite = off;
        }
    }

    public void OnClickMute()
    {
        if (volumenOn)
        {
            AudioListener.pause = true;
            GetComponent<Image>().sprite = off;
            volumenOn = false;
        }
        else
        {
            AudioListener.pause = false;
            GetComponent<Image>().sprite = on;
            volumenOn = true;
        }
    }
    
}
