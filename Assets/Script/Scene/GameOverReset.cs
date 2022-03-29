using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverReset : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
        }
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
