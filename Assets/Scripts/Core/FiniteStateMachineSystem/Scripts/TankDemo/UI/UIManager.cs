using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// The UIManager,show the info on canvas when game is running
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager ms_Instance;

    //show ai tank hp value
    public Text showAIHPText;

    //show ai tank current state
    public Text showAIStateText;

    public RectTransform showRestartPanel;

    public Toggle showPlayerView;

    public Toggle showAIView;

    public Button restartButton;

    public Button quitButton;

    public event Action<bool> showPlayerViewEvent;

    public event Action<bool> showAIViewEvent;

    public static UIManager GetInstance()
    {
        return ms_Instance;
    }

    public void Awake()
    {
        ms_Instance = this;

        showPlayerView.onValueChanged.AddListener(delegate(bool value)
        {
            if (showPlayerViewEvent != null)
            {
                showPlayerViewEvent.Invoke(value);
            }
        });


        showAIView.onValueChanged.AddListener(delegate(bool value)
        {
            if (showAIViewEvent != null)
            {
                showAIViewEvent.Invoke(value);
            }
        });

        restartButton.onClick.AddListener(delegate() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        quitButton.onClick.AddListener(delegate()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif
        });
    }

    public void UpdateAIHP_AIState_DisplayInfo(string hp,string state)
    {
        showAIHPText.text = hp;

        showAIStateText.text = state;
    }

    public void ShowRestartPanel()
    {
        if (showRestartPanel.gameObject.activeInHierarchy == false)
        {
            showRestartPanel.gameObject.SetActive(true);
        }
    }

}
