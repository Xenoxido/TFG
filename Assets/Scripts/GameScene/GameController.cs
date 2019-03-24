using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private string namechar;
    [SerializeField] private GameObject ethan;
    [SerializeField] private GameObject atenea;
    // Start is called before the first frame update
    void Start()
    {
        namechar = PlayerPrefs.GetString("character");
        switch (namechar)
        {
            case "Ethan":
                Instantiate(ethan);
                break;
            case "Atenea":
                Instantiate(atenea);
                break;
            default:
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
