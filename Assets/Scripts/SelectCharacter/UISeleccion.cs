using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISeleccion : MonoBehaviour
{
    [SerializeField] private CharController[] characters;
    [SerializeField] private GameObject selection;
    [SerializeField] private Button buttonSelection;
    [SerializeField] private Text character;
    [SerializeField] private Text life;
    [SerializeField] private Text strength;
    [SerializeField] private Text speed;
    [SerializeField] private SelectionManager selectionManager;
    public bool second = false;
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
        if (Input.GetKeyDown(KeyCode.Escape) && !second)
        {
            onClickAtras();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && second)
        {
            onClickSecondAtras();
        }

        if (Input.GetKeyDown(KeyCode.Return) && !second && buttonSelection.gameObject.active)
        {
            onClickSeleccionado();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && second)
        {
            OnClickSecondSelected();
        }

        float angleRot = selection.transform.eulerAngles.y;
        float delay = angleRot % 90;
        //Debug.Log("Rotation: " + delay);

        //Esta guarda es para saber cuando esta dentro de la rotacion delayRot que permite hacer visible el boton de seleccion,
        //el 0 y 90 son casos especiales, para ello esta la primera condición.
        if ((delay <= 90.0f && delay > (90.0f-delayRot)) || (delay < delayRot && delay > (-delayRot)))
        {
            buttonSelection.interactable = true;
            if (selectionManager.characterSelected == 1)
            {
                character.text = "Ethan";
                life.text = "Life: " + characters[0].life.ToString();
                strength.text = "Strength: " + ((characters[0].JDamage + characters[0].KDamage + characters[0].UDamage + characters[0].IDamage) / 4.0f).ToString();
                speed.text = "Speed: " + (100-((characters[0].Jtime + characters[0].Ktime + characters[0].Utime + characters[0].Itime)*10)).ToString();

            }
            else if (selectionManager.characterSelected == 2)
            {
                character.text = "Atenea";
                life.text = "Life: " + characters[1].life.ToString();
                strength.text = "Strength: " + ((characters[1].JDamage + characters[1].KDamage + characters[1].UDamage + characters[1].IDamage) / 4.0f).ToString();
                speed.text = "Speed: " + (100-((characters[1].Jtime + characters[1].Ktime + characters[1].Utime + characters[1].Itime)*10)).ToString();
            }
            else
            {
                character.text = "Eve";
                life.text = "Life: " + characters[2].life.ToString();
                strength.text = "Strength: " + ((characters[2].JDamage + characters[2].KDamage + characters[2].UDamage + characters[2].IDamage) / 4.0f).ToString();
                speed.text = "Speed: " + (100-((characters[2].Jtime + characters[2].Ktime + characters[2].Utime + characters[2].Itime)*10)).ToString();

            }
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
    public void onClickSecondAtras()
    {
        SceneManager.LoadScene("SelectCharacter");
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
        if(PlayerPrefs.GetString("Modo")== "Solo") SceneManager.LoadScene("MapSelector");
        else if(PlayerPrefs.GetString("Modo")== "Versus") SceneManager.LoadScene("SelectSecondCharacter");
    }

    public void OnClickSecondSelected()
    {
        PlayerPrefs.SetString("secondCharacter", character.text);
        PlayerPrefs.SetString("WinPlayer1", "No");
        PlayerPrefs.SetString("WinPlayer2", "No");
        PlayerPrefs.SetString("WinEnemy1", "No");
        PlayerPrefs.SetString("WinEnemy2", "No");
        PlayerPrefs.SetString("PlayerVictory", "No");
        PlayerPrefs.SetString("EnemyVictory", "No");
        SceneManager.LoadScene("MapSelector");
    }
}
