using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuModel
{
    public MainMenuModel()
    {

    }

    public void OnPlayButtonClicked()
    {
        GameLauncher.Instance.Launch(Fusion.GameMode.AutoHostOrClient);
    }
}
