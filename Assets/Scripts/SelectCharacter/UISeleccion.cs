using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISeleccion : MonoBehaviour
{
    [SerializeField] private GameObject selection;
    [SerializeField] private Button buttonSelection;
    [SerializeField] private Text character;
    [SerializeField] private SelectionManager selectionManager;
    //[SerializeField] private Character CharacterSelect;

    private float delayRot;
    // Start is called before the first frame update
    void Start()
    {
        character.text = "Atenea";
        delayRot = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {       
        float angleRot = selection.transform.eulerAngles.y;
        float delay = angleRot % 90;
        //Debug.Log("Rotation: " + delay);

        //Esta guarda es para saber cuando esta dentro de la rotacion delayRot que permite hacer visible el boton de seleccion,
        //el 0 y 90 son casos especiales, para ello esta la primera condición.
        if ((delay <= 90.0f && delay > (90.0f-delayRot)) || (delay < delayRot && delay > (-delayRot)))
        {
            buttonSelection.interactable = true;
            if(selectionManager.characterSelected == 1) character.text = "Ethan";
            else if (selectionManager.characterSelected == 2) character.text = "Atenea";
            else character.text = "Eve";
        }
        else
        {
            //Debug.Log("Entro");
            buttonSelection.interactable = false;
        }
    }

    public void onClickAtras()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void onClickSeleccionado()
    {
        PlayerPrefs.SetString("character", character.text);
        PlayerPrefs.SetString("WinPlayer1", "No");
        PlayerPrefs.SetString("WinPlayer2", "No");
        PlayerPrefs.SetString("WinEnemy1", "No");
        PlayerPrefs.SetString("WinEnemy2", "No");
        PlayerPrefs.SetString("PlayerVictory", "No");
        PlayerPrefs.SetString("EnemyVictory", "No");

        SceneManager.LoadScene("GameScene");
    }
}
