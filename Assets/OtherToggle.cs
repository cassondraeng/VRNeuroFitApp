using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherToggle : MonoBehaviour
{

    [SerializeField] private GameObject MyOther;
    [SerializeField] private GameObject MyLabel;

    private void Start()
    {
    }
    void Update()
    {
        string s = MyLabel.GetComponent<Text>().text;
        MyOther.SetActive(s == "Other" || s == "Yes, this type:");
    }
}
