using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private string namechar;
    private string namenemy;
    [SerializeField] private GameObject ethan;
    [SerializeField] private GameObject atenea;
    [SerializeField] private GameObject eve;

    [SerializeField] private GameObject derrick;


    // Start is called before the first frame update
    void Start()
    {
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
