using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class media_initialize_questions : MonoBehaviour
{
    [SerializeField] private questionaire_strings strings;
    [SerializeField] private GameObject question_prefab;
    [SerializeField] private GameObject sub_question_prefab;
    [SerializeField] private Button next_button;
    [SerializeField] string scene_to_go_to;
    [SerializeField] private bool shouldSaveData;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawner());
    }

    IEnumerator spawner()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine(lerp_alpha());
        bool shouldBeHidden = false;

        for (int i = 0; i < strings.questions.Length; i++)
        {
            if (i % 2 == 0 ) { // not a sub-question
                var q = Instantiate(question_prefab, transform);
                shouldBeHidden = false;
                LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
                var sq = q.GetComponent<media_setup_questions>();
                yield return new WaitUntil(() => sq.prev_selected != -1);

                // Hide sub-question
                if (sq.prev_selected == 0) shouldBeHidden = true;
            }
            else { // at a sub-question
                var q = Instantiate(sub_question_prefab, transform);
                LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
                var sq = q.GetComponent<media_setup_questions>();

                // Set sub-question data to 0 and hide it
                if (shouldBeHidden) {
                    yield return null; // wait for sibling index to be set
                    // Call the default 0 onclick
                    sq.set_button(sq.index, 0)();
                    q.SetActive(false);
                }

                else yield return new WaitUntil(() => sq.prev_selected != -1);
                shouldBeHidden = false;
            }
        }

        Instantiate(next_button, transform).onClick.AddListener(() =>
        {
            if (shouldSaveData) {
                var main = FindObjectOfType<main>();
                main.saveFullTest();
                Destroy(main);
            }
            
            //create a csv and call Save
            SceneManager.LoadScene(scene_to_go_to);
        });
        
    }

    IEnumerator lerp_alpha()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        while (cg.alpha < 1)
        {
            cg.alpha += Time.deltaTime;
            yield return null;
        }

        cg.alpha = 1;
    }
}
