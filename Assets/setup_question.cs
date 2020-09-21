using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.Events;

public class setup_question : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Button[] buttons;

    [SerializeField]
    questionaire_strings strings;

    [SerializeField] private InputSend not_adrians;

    public int prev_selected = -1; 
    
    // Start is called before the first frame update
    void Start()
    {
        int index = transform.GetSiblingIndex();
        text.text = strings.questions[index];
      
        for (int i = 0; i < buttons.Length; i++)
        {
            set_normal(i);
            buttons[i].onClick.AddListener(set_button(index,i));
           
        }
    }

    UnityAction set_button(int sibling_index, int i)
    {
        return () =>
        {
            if (prev_selected != -1)
                set_normal(prev_selected);
            set_selected(i);
            prev_selected = i;

            int index = i + 1;

            //i + 1 is number on the button because its one indexed
            not_adrians.set_happy((HeaderType) sibling_index, index);
        };
    }


    void set_normal(int i)
    {
        buttons[i].targetGraphic.color = buttons[i].colors.normalColor;
    }

    void set_selected(int i)
    {
        buttons[i].targetGraphic.color = buttons[i].colors.pressedColor;
    }

}
