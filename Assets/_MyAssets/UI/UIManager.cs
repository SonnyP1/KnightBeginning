using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] Button _startBtn;
    [SerializeField] Button _exitBtn;
    [SerializeField] GameObject _mainMenuUI;

    [Header("Lose Menu")]
    [SerializeField] GameObject _loseScreen;
    [SerializeField] Button _retryBtn;

    [Header("InGameUI")]
    [SerializeField] GameObject _gameUI;

    //private
    private HealthComp _playerHealthComp;

    private void OnDestroy()
    {
        _playerHealthComp.onDeath -= DeathScreen;
    }

    private void Start()
    {
        //Find PlayerHealth
        _playerHealthComp = FindObjectOfType<HealthComp>();
       
        //Reset Game
        Time.timeScale = 1;
        _playerHealthComp.SetDeathCamPriority(0);
       
        //Find Player death subscribe to death
        _playerHealthComp.onDeath += DeathScreen;

        //Set Up Buttons Clicks
        _startBtn.onClick.AddListener(StartBtnClick);
        _exitBtn.onClick.AddListener(ExitOnClick);

        _retryBtn.onClick.AddListener(RetryBtnClick);

        //Set Menus
        _mainMenuUI.SetActive(true);
        _gameUI.SetActive(false);
        _loseScreen.SetActive(false);
    }

    private void StartBtnClick()
    {
        Debug.Log("Start now!");
        _mainMenuUI.SetActive(false);
        _gameUI.SetActive(true);
        FindObjectOfType<EnemySpawner>().enabled = true;
    }
    private void ExitOnClick()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    private void RetryBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }

    public void DeathScreen()
    {
        _mainMenuUI.SetActive(false);
        _loseScreen.SetActive(true);
    }
}
