using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrePost : MonoBehaviour
{
    [SerializeField] private boolVal pre;
    [SerializeField] private boolVal post;

    [SerializeField] private Dropdown dropdown;

    private void Start() {
        dropdown = GetComponent<Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        post.val = dropdown.value == 1;
        pre.val = dropdown.value == 0;
    }
}
