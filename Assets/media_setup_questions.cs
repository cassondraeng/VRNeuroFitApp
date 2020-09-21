using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.Events;

public class media_setup_questions : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Button[] buttons;

    [SerializeField]
    questionaire_strings strings;

    [SerializeField] private InputSend not_adrians;
    [SerializeField] private bool isSubQuestion = false;

    public int prev_selected = -1; 
    public int index = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        index = transform.GetSiblingIndex();
        text.text = strings.questions[index];
      
        for (int i = 0; i < buttons.Length; i++)
        {
            set_normal(i);
            buttons[i].onClick.AddListener(set_button(index,i));
        }
    }

    public UnityAction set_button(int sibling_index, int i)
    {
        return () =>
        {
            if (prev_selected != -1)
                set_normal(prev_selected);
            set_selected(i);

            if (isSubQuestion) {
                prev_selected = i;
                not_adrians.set_happy((HeaderType) sibling_index, i);
            } else {
                prev_selected = i;
                not_adrians.set_happy((HeaderType) sibling_index, i);

                // See if we need to hide or unhide the sub-questions
                if (transform.parent.childCount < sibling_index + 2) return;
                GameObject g = transform.parent.GetChild(index + 1).gameObject;

                // reset hidden object to unhidden if 1 clicked and vice versa
                if (i == 0) {
                    if (g.activeSelf) {
                        // hide next object
                        var sq = g.GetComponent<media_setup_questions>();
                        sq.set_button(sq.index, 0) ();
                        g.SetActive(false);
                    }

                } else {
                    if (!g.activeSelf) {
                        g.SetActive(true);
                    }
                }
            }
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
