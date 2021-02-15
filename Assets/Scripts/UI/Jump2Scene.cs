using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jump2Scene : MonoBehaviour
{

    [SerializeField] private string scene;
    
    public void Jump() {
        SceneManager.LoadScene(scene);

    }
}
