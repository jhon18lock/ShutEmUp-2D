using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    float originalTimeScale;

    // Start is called before the first frame update
    void Start()
    {
        originalTimeScale = Time.timeScale;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameController.Instance.IsGamePaused = true;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = originalTimeScale;
        GameController.Instance.IsGamePaused = false;
    }

    //pausar despausar por el boton
    public void TooglePause()
    {
        if (GameController.Instance.IsGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

}
