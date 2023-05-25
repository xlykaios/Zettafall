using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;


    public void NewGame(int SceneID)
    {
        SceneManager.LoadScene(SceneID); 
    }

    public void Continue(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }

    public void Options(int SceneID)
    { 
        SceneManager.LoadScene(SceneID);
    }

    public void Gallery(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }

    public void Quit(int SceneID) {
        SceneManager.LoadScene(SceneID);
    }
}
