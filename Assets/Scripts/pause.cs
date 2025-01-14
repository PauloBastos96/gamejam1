using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // ReferÍncia ao painel de pausa

    private bool isPaused = false;

    private void OnEnable()
    {
        TogglePause();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pausa o jogo
            pausePanel.SetActive(true); // Ativa o painel de pausa
        }
        else
        {
            Time.timeScale = 1f; // Despausa o jogo
            pausePanel.SetActive(false); // Desativa o painel de pausa
        }
    }

    public void Restart()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
