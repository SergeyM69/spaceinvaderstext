using SpaceInvaders.Events;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text scoreText;

    private void Awake()
    {
        tutorialPanel.SetActive(true);
        EventsHub.onFireButtonStateChanged += UiManager_OnFinshTutorial;
        EventsHub.onScoreChanged += UiManager_OnGameScoreChange;
        EventsHub.onGameOver += UiManager_OnGameOver;
    }

    private void UiManager_OnFinshTutorial(bool isActive)
    {
        if (isActive)
        {
            tutorialPanel.gameObject.SetActive(false);
            EventsHub.onFireButtonStateChanged -= UiManager_OnFinshTutorial;
            EventsHub.InvokeTutorialCompleted();
        }
    }

    private void UiManager_OnGameOver()
    {
        gameOverPanel.SetActive(true);
        EventsHub.onFireButtonStateChanged += UiManager_OnGameOverRestart;
    }

    private void UiManager_OnGameOverRestart(bool isActive)
    {
        if (isActive)
        {
            EventsHub.InvokeLevelRestartRequested();
            gameOverPanel.SetActive(false);
            EventsHub.onFireButtonStateChanged -= UiManager_OnGameOverRestart;
        }
    }    
    
    private void UiManager_OnGameScoreChange(int score)
    {
        scoreText.text = score.ToString();
    }
}
