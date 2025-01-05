using UnityEngine;
using TMPro;
using UnityEngine.UI;

//* Formatted and Commented
public class LogicFB : MonoBehaviour
{
    #region Variables
    [Header("Game Objects")]
    [Space(10)]

    [Tooltip("Flappy Bird Menu UI")]
    [SerializeField]
    GameObject FBMenuUI;

    [Tooltip("Flappy Bird Game UI")]
    [SerializeField]
    GameObject FBGameUI;

    [Tooltip("The GameObject that appears when the game is paused.")]
    [SerializeField]
    GameObject gamePausedScreen;

    [Tooltip("The GameObject that appears when the game is over.")]
    [SerializeField]
    GameObject gameOverScreen;

    [Tooltip("Pause Button\nShows Game Paused Screen")]
    [SerializeField]
    Button PauseButton;


    [Header("Score Management")]
    [Space(10)]

    [Tooltip("Score\nResets to 0 when game is restarted")]
    public int score;

    [Tooltip("Score Text\nDisplays score value")]
    [SerializeField]
    TextMeshProUGUI scoretxt;

    [Tooltip("HighScore Text\nDisplays HighScore value")]
    [SerializeField]
    TextMeshProUGUI HighScoreText;

    [Tooltip("Reset HighScore\nIndicates whether the 'HighScore' in PlayerPrefs should be reset to 0.")]
    [SerializeField]
    bool resetHighScore;

    // Static bool to check if game is started, paused of over
    public static bool isGameStarted;
    #endregion


    #region Methods

    //* Runs before the game starts/before the first frame
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

        // Load Flappy Bird Menu UI (Show) and hide everything else
        //FlappyBird.LoadFbGame(FBMenuUI, FBGameUI, gamePausedScreen, gameOverScreen, true, false, false, false);
        HighScoreText.text = PlayerPrefsManager.HighScorePlayerPrefs(score, "BB_HighScore", resetHighScore).ToString();
    }

    //* Called after Start Method, and Runs once every frame
    void Update()
    {
        HighScoreText.text = PlayerPrefsManager.HighScorePlayerPrefs(score, "FB_HighScore", false).ToString();
    }

    //* Adds Custom value to the score
    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoretxt.text = score.ToString();
    }
    #endregion

    #region Scene Management

    //*  Loads Flappy Bird Game UI and starts the game
    public void StartGame()
    {
        FlappyBird.LoadFbGame(FBMenuUI, FBGameUI, gamePausedScreen, gameOverScreen, false, true, false, false);
        isGameStarted = true;
    }

    //* Pauses/Resumes the game
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

    //* Reloads FlappyBird Game UI and starts the game
    public void RestartGame()
    {
        FlappyBird.LoadFbMainMenu();
        ClassManager.FB_restarted = true;
    }

    //* Loads Flappy Bird Game Over Screen (shows)
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        PauseButton.interactable = false;
    }

    //* Loads Flappy Bird Main Menu and reset time scale
    public void FBMainMenu()
    {
        FlappyBird.LoadFbMainMenu();
        Time.timeScale = 1.0f;
    }

    //* Loads Main Menu
    public void ReturnToMainMenu()
    {
        MainMenu.LoadMainMenu();
    }
    #endregion
}