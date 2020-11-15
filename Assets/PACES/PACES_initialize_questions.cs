using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PACES_initialize_questions : MonoBehaviour
{
    [SerializeField] private questionaire_strings strings;
    [SerializeField] private PACES_indexes types;
    [SerializeField] private GameObject questionTypeOne_prefab;
    [SerializeField] private GameObject questionTypeTwo_prefab;
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
        for (int i = 0; i < strings.questions.Length; i++)
        {
            var q = Instantiate(new GameObject());
            if (types.indexes[i] == 0)
            {
                q = Instantiate(questionTypeOne_prefab, transform);
            }
            else
            {
                q = Instantiate(questionTypeTwo_prefab, transform);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

            yield return new WaitUntil(() => q.GetComponent<setup_question>().prev_selected != -1);
        }

        Instantiate(next_button, transform).onClick.AddListener(() =>
        {
            if (shouldSaveData)
            {
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
