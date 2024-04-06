using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // Referência ao painel de pausa

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pausa o jogo
            Debug.Log("Jogo pausado.");
            pausePanel.SetActive(true); // Ativa o painel de pausa
        }
        else
        {
            Time.timeScale = 1f; // Despausa o jogo
            Debug.Log("Jogo despausado.");
            pausePanel.SetActive(false); // Desativa o painel de pausa
        }
    }
}
