using UnityEngine;
using TMPro;
using UnityEngine.UI;

//# Formatted and Commented

/// <summary>
/// Manages the game's core logic including score tracking, game start, pause, and game over functionality.
/// It also manages the UI elements for the game, such as displaying the current score, high score, and handling the main menu.
/// </summary>
public class LogicFB : MonoBehaviour
{
    #region Variables

    [Header("Game Objects")]
    [Space(10)]

    [Tooltip("Flappy Bird Menu UI. Displays the game's main menu.")]
    [SerializeField]
    GameObject FBMenuUI;

    [Tooltip("Flappy Bird Game UI. Displays the in-game UI while playing.")]
    [SerializeField]
    GameObject FBGameUI;

    [Tooltip("The GameObject displayed when the game is paused.")]
    [SerializeField]
    GameObject gamePausedScreen;

    [Tooltip("The GameObject displayed when the game is over.")]
    [SerializeField]
    GameObject gameOverScreen;

    [Tooltip("Pause Button. Toggles the game paused state.")]
    [SerializeField]
    Button PauseButton;

    [Header("Score Management")]
    [Space(10)]

    [Tooltip("Current score of the player. Resets to 0 when the game restarts.")]
    public int score;

    [Tooltip("Text component to display the current score.")]
    [SerializeField]
    TextMeshProUGUI scoretxt;

    [Tooltip("Text component to display the player's high score.")]
    [SerializeField]
    TextMeshProUGUI highscoreText;

    [Tooltip("Boolean to reset the high score saved in PlayerPrefs to 0.")]
    [SerializeField]
    bool resetHighScore;

    [Tooltip("Indicates whether the game is currently started.")]
    public static bool isGameStarted;

    #endregion

    #region Methods

    /// <summary>
    /// Initializes the game state based on whether it's a restart or a fresh start.
    /// </summary>
    void Awake()
    {
        if (ClassManager.FB_restarted)
        {
            FlappyBird.LoadFbGame(FBMenuUI, FBGameUI, gamePausedScreen, gameOverScreen, false, true, false, false);
            isGameStarted = true;
            Time.timeScale = 1f;
            ClassManager.FB_restarted = false;
        }
        else
        {
            FlappyBird.LoadFbGame(FBMenuUI, FBGameUI, gamePausedScreen, gameOverScreen, true, false, false, false);
        }

        // Display the high score from PlayerPrefs
        highscoreText.text = PlayerPrefsManager.HighScorePlayerPrefs(score, "BB_HighScore", resetHighScore).ToString();
    }

    /// <summary>
    /// Updates the high score UI every frame.
    /// </summary>
    void Update()
    {
        highscoreText.text = PlayerPrefsManager.HighScorePlayerPrefs(score, "FB_HighScore", false).ToString();
    }

    /// <summary>
    /// Adds a specified value to the player's score and updates the score UI.
    /// </summary>
    /// <param name="scoreToAdd">The value to add to the current score.</param>
    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoretxt.text = score.ToString();
    }

    #endregion

    #region Scene Management

    /// <summary>
    /// Starts the game by enabling the game UI and resuming the game loop.
    /// </summary>
    public void StartGame()
    {
        FlappyBird.LoadFbGame(FBMenuUI, FBGameUI, gamePausedScreen, gameOverScreen, false, true, false, false);
        isGameStarted = true;
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Toggles the paused state of the game and updates the pause UI.
    /// </summary>
    public void GamePaused()
    {
        if (gamePausedScreen.activeSelf)
        {
            gamePausedScreen.SetActive(false);
            isGameStarted = true;
            Time.timeScale = 1f;
        }
        else
        {
            gamePausedScreen.SetActive(true);
            isGameStarted = false;
            Time.timeScale = 0f;
        }
    }

    /// <summary>
    /// Restarts the game by reloading the game UI and setting the necessary state.
    /// </summary>
    public void RestartGame()
    {
        FlappyBird.LoadFbMainMenu();
        ClassManager.FB_restarted = true;
    }

    /// <summary>
    /// Ends the game by showing the game over screen and disabling pause functionality.
    /// </summary>
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        PauseButton.interactable = false;
    }

    /// <summary>
    /// Returns to the Flappy Bird main menu and pauses the game.
    /// </summary>
    public void FBMainMenu()
    {
        FlappyBird.LoadFbMainMenu();
        Time.timeScale = 0;
    }

    /// <summary>
    /// Returns to the main menu of the application.
    /// </summary>
    public void ReturnToMainMenu()
    {
        MainMenu.LoadMainMenu();
    }

    #endregion

}