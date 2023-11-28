using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    [SerializeField] private GameObject viewManagerPrefab;
    [SerializeField] private GameObject audioManagerPrefab;
    [SerializeField] private GameObject gameLauncherPrefab;

    ViewManager viewManager;

    private void Start()
    {
        Application.targetFrameRate = 120;
        StartLoadingFlow();
    }

    private void StartLoadingFlow()
    {
        Locator.Instance.Register(this);

        var viewManagerObject = Instantiate(viewManagerPrefab);
        viewManager = viewManagerObject.GetComponent<ViewManager>();
        Locator.Instance.Register(viewManager);

        var audioManager = Instantiate(audioManagerPrefab).GetComponent<AudioManager>();
        Locator.Instance.Register(audioManager);
        
        var gameLauncher = Instantiate(gameLauncherPrefab).GetComponent<GameLauncher>();
        Locator.Instance.Register(gameLauncher);

        viewManager.LoadView("MainMenuPresenter");
    }
}