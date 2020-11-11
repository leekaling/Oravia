using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            quitGame();
        }
    }
    public void playGame()
    {
        SceneManager.LoadScene("WindKingdom3D");
    }
    public void quitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            Application.Quit();
    }
}
