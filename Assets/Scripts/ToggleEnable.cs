using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEnable : MonoBehaviour
{

    [SerializeField] private boolVal val;

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(val.val);
    }
}
