using UnityEngine;
using UnityEngine.SceneManagement;

//# Formatted and Commented

/// <summary>
/// This class handles the scene management for various game modes.
/// </summary>
public class ScManager : MonoBehaviour
{
    // Empty for now; this class can be extended later with additional functionalities.
}

/// <summary>
/// Handles operations related to the Main Menu, such as loading the scene and adjusting screen orientation.
/// </summary>
public class MainMenu
{
    /// <summary>
    /// Loads the Main Menu scene and sets the screen orientation to portrait mode.
    /// </summary>
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Loads the main menu scene.
        ScreenOrientationManager.SetPortrait(); // Sets the screen orientation to portrait.
    }
}

/// <summary>
/// Manages the Flappy Bird game scene and its UI elements.
/// </summary>
public class FlappyBird
{
    /// <summary>
    /// Loads the Flappy Bird main menu scene and sets the screen orientation to landscape (auto).
    /// </summary>
    public static void LoadFbMainMenu()
    {
        SceneManager.LoadScene("FlappyBird"); // Loads the Flappy Bird main menu scene.
        ScreenOrientationManager.SetLandscapeAuto(); // Sets the screen orientation to landscape (auto).
    }

    /// <summary>
    /// Activates or deactivates specific UI elements for the Flappy Bird game.
    /// </summary>
    /// <param name="FBMenuUI">Reference to the Main Menu UI GameObject.</param>
    /// <param name="FBGameUI">Reference to the Game UI GameObject.</param>
    /// <param name="FBgamePausedScreen">Reference to the Game Paused screen GameObject.</param>
    /// <param name="FBgameOverScreen">Reference to the Game Over screen GameObject.</param>
    /// <param name="SetFBMenuUIActive">Whether to activate or deactivate the Main Menu UI.</param>
    /// <param name="SetFBGameUIActive">Whether to activate or deactivate the Game UI.</param>
    /// <param name="SetFBgamePausedScreenActive">Whether to activate or deactivate the Game Paused screen.</param>
    /// <param name="SetFBGameOverScreenActive">Whether to activate or deactivate the Game Over screen.</param>
    public static void LoadFbGame(GameObject FBMenuUI, GameObject FBGameUI, GameObject FBgamePausedScreen, GameObject FBgameOverScreen,
                                  bool SetFBMenuUIActive, bool SetFBGameUIActive, bool SetFBgamePausedScreenActive, bool SetFBGameOverScreenActive)
    {
        FBMenuUI.SetActive(SetFBMenuUIActive); // Set the Main Menu UI active state.
        FBGameUI.SetActive(SetFBGameUIActive); // Set the Game UI active state.
        FBgamePausedScreen.SetActive(SetFBgamePausedScreenActive); // Set the Game Paused screen active state.
        FBgameOverScreen.SetActive(SetFBGameOverScreenActive); // Set the Game Over screen active state.
    }
}

/// <summary>
/// Handles operations related to the Brick Breaker game, such as loading the scene and setting the screen orientation.
/// </summary>
public class BrickBreaker
{
    /// <summary>
    /// Loads the Brick Breaker main menu scene and sets the screen orientation to portrait mode.
    /// </summary>
    public static void LoadBbMainMenu()
    {
        SceneManager.LoadScene("BrickBreaker"); // Loads the Brick Breaker main menu scene.
        ScreenOrientationManager.SetPortrait(); // Sets the screen orientation to portrait.
    }
}