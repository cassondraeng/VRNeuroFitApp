using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyYouFools : MonoBehaviour
{
    public HeaderType H;

    [SerializeField]
    InputSend cool;

    private void Start()
    {
        var t = GetComponentInChildren<Dropdown>();
        //For Dropdowns, we need to store an integer value so that data analysis is easier.
        if (t)
            GetComponentInChildren<Dropdown>().onValueChanged.AddListener(selection => cool.set_happy(H,t.value));
        var z = GetComponentInChildren<InputField>();
        if (z)
            GetComponentInChildren<InputField>().onValueChanged.AddListener(given =>
            {
                //print(given);
                cool.set_happy(H, given);
            });
    }
}
