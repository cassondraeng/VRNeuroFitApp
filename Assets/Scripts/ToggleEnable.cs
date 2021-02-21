using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleEnable : MonoBehaviour
{

    [SerializeField] private main.track trackingVal;

    private main script;

    private Image myImage;

    private void Start() {
        script = GameObject.Find("Script").GetComponent<main>();
        myImage = GetComponent<Image>();

        myImage.enabled = script.banana[(int)trackingVal];
    }
}
