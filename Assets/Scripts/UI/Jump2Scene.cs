using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jump2Scene : MonoBehaviour
{
    [SerializeField] private string scene;
    public GameObject prepostMenu;
    public Dropdown dropdown;

    private void Start()
    {
        dropdown = prepostMenu.GetComponent<Dropdown>();
    }

    public void Jump() {
        if (dropdown != null && dropdown.value == 2) SceneManager.LoadScene("DEMOGRAPHICS");
        else SceneManager.LoadScene(scene);
    }
}
