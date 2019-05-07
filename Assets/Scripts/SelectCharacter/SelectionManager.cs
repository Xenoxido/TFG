using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Vector3 targetRot;
    public Vector3 currentAngle;

    public int characterSelected;
    public int totalCharacter;

    [SerializeField] private GameObject music;
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("BattleSound") != null) Destroy(GameObject.FindGameObjectWithTag("BattleSound"));
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1) Destroy(music);
        else DontDestroyOnLoad(music);
    }

    private void Start()
    {
        characterSelected = 2;
        totalCharacter = 3;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D)))
        {
            currentAngle = transform.eulerAngles;
            if (characterSelected < totalCharacter)
            {
                targetRot = targetRot + new Vector3(0, 90, 0);
                characterSelected++;
            }
            else
            {
                targetRot = targetRot - new Vector3(0, 180, 0);
                characterSelected = 1;
            }
        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A)))
        {
            currentAngle = transform.eulerAngles;
            if(characterSelected > 1)
            {
                targetRot = targetRot - new Vector3(0, 90, 0);
                characterSelected--;
            }
            else
            {
                targetRot = targetRot + new Vector3(0, 180, 0);
                characterSelected = totalCharacter;
            }
        }

        //Debug.Log("Personaje: " + characterSelected);

        currentAngle = new Vector3(0, Mathf.LerpAngle(currentAngle.y, targetRot.y, 2.0f * Time.deltaTime), 0);
        transform.eulerAngles = currentAngle;
    }

}
