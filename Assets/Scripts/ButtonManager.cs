using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void OpenGameBtn(string openGameLevel)
    {
        SceneManager.LoadScene(openGameLevel);
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }
}
