using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool IsPaused { get; private set; }

    public GameObject m_PausePanel;
    public KeyCode m_KeyToPause;

    public void Show()
    {
        m_PausePanel.SetActive(true);
        IsPaused = true;
        Time.timeScale = 0.0f;
    }
    
    public void Hide()
    {
        m_PausePanel.SetActive(false);
        IsPaused = false;
        Time.timeScale = 1.0f;
    }

    public void Toggle()
    {
        if (IsPaused)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_KeyToPause))
        {
            Toggle();
        }
    }
}
