using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlyYouFools : MonoBehaviour
{
    public HeaderType H;

    [SerializeField]
    InputSend cool;

    private void OnEnable()
    {
        var t = GetComponentInChildren<Dropdown>();
        //For Dropdowns, we need to store an integer value so that data analysis is easier.
        if (t)
        {
            cool.set_happy(H, t.value);
            t.onValueChanged.RemoveAllListeners();
            t.onValueChanged.AddListener(selection => cool.set_happy(H, t.value));
        }
            
        var z = GetComponentInChildren<InputField>();
        if (z)
        {
            z.onValueChanged.RemoveAllListeners();
            z.onValueChanged.AddListener(given => cool.set_happy(H, given));
        }
            
        var b = GetComponentInChildren<TMP_InputField>();
        if (b)
        {
            b.onValueChanged.RemoveAllListeners();
            b.onValueChanged.AddListener(given => cool.set_happy(H, given));
        }

        var m = GetComponentInChildren<TMP_Dropdown>();
        if (m)
        {
            cool.set_happy(H, m.value);
            m.onValueChanged.RemoveAllListeners();
            m.onValueChanged.AddListener(selection => cool.set_happy(H, m.value));
        }
            
    }
}
