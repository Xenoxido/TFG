using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class MapSelect : MonoBehaviour
{
    public string name;
    [SerializeField] private Material mat;
    [SerializeField] private Text text;
    [SerializeField] private MapSelectorController mapSelector;
    // Start is called before the first frame update
    void Start()
    {
        text.text = name;
    }

    public void OnClickMap()
    {
        RenderSettings.skybox = mat;
        mapSelector.SetName(name);

    }
    
}
