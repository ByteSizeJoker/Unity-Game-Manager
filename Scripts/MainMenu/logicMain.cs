using UnityEngine;
using System.Collections;

//# Formatted and Commented

/// <summary>
/// Handles the main game logic, including UI transitions, player preferences, and game scene loading.
/// </summary>
public class logicMain : MonoBehaviour
{
    [Header("UI Related Game Objects")]
    [Tooltip("GameObject for the Play screen.")]
    public GameObject playScreen;

    [Tooltip("GameObject for the Game Selection screen.")]
    public GameObject gameSelection;

    [Tooltip("Panel 1 GameObject used in UI transitions.")]
    public GameObject panel1;

    [Tooltip("Panel 2 GameObject used in UI transitions.")]
    public GameObject panel2;

    [Tooltip("Welcome screen GameObject, shown when the app is first launched.")]
    public GameObject _WelcomeScreen;

    private Vector3 initialPanel1Pos, initialPanel2Pos;

    /// <summary>
    /// Called when the script is initialized.
    /// Initializes the positions of panels and clears PlayerPrefs for fresh data. Also sets the target frame rate.
    /// </summary>
    public void Awake()
    {
        initialPanel1Pos = panel1.GetComponent<RectTransform>().anchoredPosition;
        initialPanel2Pos = panel2.GetComponent<RectTransform>().anchoredPosition;

        if (deletePlayerPrefs) PlayerPrefs.DeleteAll(); // Clears all saved PlayerPrefs data

        WelcomeScreen(); // Shows the welcome screen if applicable
        Application.targetFrameRate = 90; // Sets the target frame rate to 90
    }

    /// <summary>
    /// Loads the main menu of the game.
    /// </summary>
    public static void LoadMainMenu()
    {
        MainMenu.LoadMainMenu();
    }

    #region UI Switching

    /// <summary>
    /// Switches between the PlayScreen and GameSelection screen with a smooth transition.
    /// </summary>
    public void TransitionBetPlayAndGame()
    {
        if (playScreen.activeSelf)
        {
            playScreen.SetActive(false);
            gameSelection.SetActive(true);
            StartCoroutine(Delay(0.25F)); // Waits for 0.25 seconds before transitioning
            panel1.GetComponent<RectTransform>().anchoredPosition = initialPanel1Pos;
        }
        else
        {
            playScreen.SetActive(true);
            gameSelection.SetActive(false);
            StartCoroutine(Delay(0.25f));
            panel2.GetComponent<RectTransform>().anchoredPosition = initialPanel2Pos;
        }
    }

    /// <summary>
    /// Shows the Welcome screen if it hasn't been shown before, based on PlayerPrefs.
    /// </summary>
    public void WelcomeScreen()
    {
        if (PlayerPrefs.GetInt("WelcomeScreen", 1) == 1)
        {
            _WelcomeScreen.SetActive(true);
            PlayerPrefs.SetInt("WelcomeScreen", 0); // Marks the welcome screen as shown
        }
        else if (PlayerPrefs.GetInt("WelcomeScreen") == 0)
        {
            _WelcomeScreen.SetActive(false); // Hides the welcome screen if already shown
        }
    }

    #endregion

    #region Miscellaneous

    /// <summary>
    /// Quits the application/game when called.
    /// </summary>
    public void quitGame()
    {
        Application.Quit(); // Exits the application
    }

    #endregion

    /// <summary>
    /// Unity's Update method, called once per frame. 
    /// Currently, it's not being used but can be useful for debugging or future code updates.
    /// </summary>
    void Update()
    {
        //debug = (PlayerPrefs.GetInt("WelcomeScreen") + " & 3"); // Debug line (unused)
    }

    /// <summary>
    /// Loads the main menu for the Flappy Bird game.
    /// </summary>
    public void LoadFlappyBird()
    {
        FlappyBird.LoadFbMainMenu();
    }

    /// <summary>
    /// Loads the main menu for the Brick Breaker game.
    /// </summary>
    public void LoadBrickBreaker()
    {
        BrickBreaker.LoadBbMainMenu();
    }

    /// <summary>
    /// A coroutine that waits for a specified amount of time before continuing execution.
    /// </summary>
    /// <param name="delay">Time to wait before continuing (in seconds).</param>
    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay); // Waits for the specified time
    }
}