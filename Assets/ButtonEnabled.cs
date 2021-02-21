using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnabled : MonoBehaviour
{
    [SerializeField] private boolVal pretest;
    [SerializeField] private boolVal posttest;

    [SerializeField] private bool isPre;
    [SerializeField] private bool isPost;

    private Button myButton;


    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        myButton.interactable = (isPre && pretest.val) || (isPost && posttest.val);
    }
}
