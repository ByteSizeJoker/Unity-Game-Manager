using UnityEngine;
using System.Collections;

public class logicMain : MonoBehaviour
{
    [Header("UI Related Game Objects")]
    public GameObject PlayScreen;
    public GameObject GameSelection;
    public GameObject Panal1, Panal2, _WelcomeScreen;
    private Vector3 initialPanal1Pos, initialPanal2Pos;
    private string debug;

    public void Awake()
    {
        initialPanal1Pos = Panal1.GetComponent<RectTransform>().anchoredPosition;
        initialPanal2Pos = Panal2.GetComponent<RectTransform>().anchoredPosition;
        PlayerPrefs.DeleteAll();
        WelcomeScreen();
        Application.targetFrameRate = 90;
    }
    public static void LoadMainMenu()
    {
        MainMenu.LoadMainMenu();
    }

    #region UI Switching
    public void TransitionBetPlayandGame()
    {   //Main
        if (PlayScreen.activeSelf)
        {
            PlayScreen.SetActive(false);
            GameSelection.SetActive(true);
            StartCoroutine(Delay(0.25F));
            Panal1.GetComponent<RectTransform>().anchoredPosition = initialPanal1Pos;
        }
        else
        {
            PlayScreen.SetActive(true);
            GameSelection.SetActive(false);
            StartCoroutine(Delay(0.25f));
            Panal2.GetComponent<RectTransform>().anchoredPosition = initialPanal2Pos;
        }
    }

    //OneTimePopup
    public void WelcomeScreen()
    {
        if (PlayerPrefs.GetInt("WelcomeScreen", 1) == 1)
        {
            _WelcomeScreen.SetActive(true);
            PlayerPrefs.SetInt("WelcomeScreen", 0);
        }
        else if (PlayerPrefs.GetInt("WelcomeScreen") == 0)
        {
            _WelcomeScreen.SetActive(false);
        }
    }
    #endregion

    #region Miscellianous
    public void quitGame()
    {
        Application.Quit();
    }
    #endregion

    void Update()
    {
        //debug = (PlayerPrefs.GetInt("WelcomeScreen") + " & 3");
    }

    public void LoadFlappyBird()
    {
        FlappyBird.LoadFbMainMenu();
    }

    public void LoadBrickBreaker() {
        BrickBreaker.LoadBbMainMenu();
    }

    private IEnumerator Delay(float delay) {
        yield return new WaitForSeconds(delay);
    }
}
