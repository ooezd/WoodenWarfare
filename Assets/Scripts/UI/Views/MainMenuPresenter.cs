using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPresenter : MonoBehaviour
{
    [SerializeField] UIButton playButton;

    MainMenuModel model;

    private void Awake()
    {
        model = new MainMenuModel();

        PrepareButtons();
    }

    private void PrepareButtons()
    {
        playButton.onClick += OnPlayButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        if (playButton.Interactable)
        {
            model.OnPlayButtonClicked();
            playButton.Interactable = false;
        }
    }

    private void OnDestroy()
    {
        playButton.onClick -= model.OnPlayButtonClicked;
    }
}