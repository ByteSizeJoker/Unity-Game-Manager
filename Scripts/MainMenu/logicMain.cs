using UnityEngine;
using System.Collections;
using TMPro;

//# Formatted and Commented

/// <summary>
/// Handles the main game logic, including UI transitions, player preferences, and game scene loading.
/// </summary>
public class logicMain : MonoBehaviour
{
    #region Variables
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

    [Header("Search Bar")]
    [Tooltip("Content Holder (Parent GameObject) for the elements to be searched in.")]
    public GameObject contentHolder;

    [Tooltip("Array of elements to be searched.")]
    public GameObject[] elements;

    [Tooltip("Search Bar GameObject")]
    public GameObject searchBar;

    public int totalElements;

    public bool deletePlayerPrefs;

    private Vector3 initialPanel1Pos, initialPanel2Pos;
    #endregion

    /// <summary>
    /// Called when the script is initialized.
    /// Initializes the positions of panels and clears PlayerPrefs for fresh data. Also sets the target frame rate.
    /// </summary>
    public void Awake()
    {
        ScreenOrientationManager.SetPortrait();
        initialPanel1Pos = panel1.GetComponent<RectTransform>().anchoredPosition;
        initialPanel2Pos = panel2.GetComponent<RectTransform>().anchoredPosition;

        if (deletePlayerPrefs) PlayerPrefs.DeleteAll(); // Clears all saved PlayerPrefs data

        WelcomeScreen(); // Shows the welcome screen if applicable
        Application.targetFrameRate = 90; // Sets the target frame rate to 90

        totalElements = contentHolder.transform.childCount;
        elements = new GameObject[totalElements];

        for (int i = 0; i < totalElements; i++)
        {
            elements[i] = contentHolder.transform.GetChild(i).gameObject;
        }
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

    public void SearchBar()
    {
        string search = searchBar.GetComponent<TMP_InputField>().text;
        int searchTxtLength = search.Length;

        int searchedElements = 0;

        foreach (GameObject ele in elements)
        {
            searchedElements++;

            if (ele.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text.Length >= searchTxtLength)
            {
                if (search.ToLower() == ele.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text.Substring(0, searchTxtLength).ToLower())
                {
                    ele.SetActive(true);
                }
                else {
                    ele.SetActive(false);
                }
            }
        }
    }




}