using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void playGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Update is called once per frame
    public void quitApplication()
    {
        quitApplication();
    }

    public void quitGame()
    {
        SceneManager.LoadScene("UiScene");
    }
}
