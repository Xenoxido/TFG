using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPopUp : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
