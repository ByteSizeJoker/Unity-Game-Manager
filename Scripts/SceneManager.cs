using UnityEngine;
using UnityEngine.SceneManagement;

public class ScManager : MonoBehaviour
{

    
}

public class MainMenu {
    //Main Menu
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ScreenOrientationManager.SetPortrait();
    }
}


//Flappy Bird
public class FlappyBird
{
    public static void LoadFbMainMenu()
    {
        SceneManager.LoadScene("FlappyBird");
        ScreenOrientationManager.SetLandscapeAuto();
    }

    public static void LoadFbGame(GameObject FBMenuUI, GameObject FBGameUI, GameObject FBgamePausedScreen, GameObject FBgameOverScreen,
                                  bool SetFBMenuUIActive, bool SetFBGameUIActive, bool SetFBgamePausedScreenActive, bool SetFBGameOverScreenActive)
    {
        FBMenuUI.SetActive(SetFBMenuUIActive);
        FBGameUI.SetActive(SetFBGameUIActive);
        FBgamePausedScreen.SetActive(SetFBgamePausedScreenActive);
        FBgameOverScreen.SetActive(SetFBGameOverScreenActive);
    }
}

public class BrickBreaker {
    public static void LoadBbMainMenu()
    {
        SceneManager.LoadScene("BrickBreaker");
        ScreenOrientationManager.SetPortrait();
    }
}