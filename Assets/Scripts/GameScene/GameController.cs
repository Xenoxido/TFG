using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private string namechar;
    [SerializeField] private GameObject ethan;
    // Start is called before the first frame update
    void Start()
    {
        namechar = PlayerPrefs.GetString("character");
        if (namechar == "Ethan") Instantiate(ethan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
