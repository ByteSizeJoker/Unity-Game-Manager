using UnityEngine;

public class ClassManager {
    public static bool FB_restarted = false;
}

//Orientation Manager
public class ScreenOrientationManager
{
    public static void SetPortrait()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SetAutoRotation(true, false, false, false);
    }
    public static void SetPortraitUpsideDown()
    {
        Screen.orientation = ScreenOrientation.PortraitUpsideDown;
        SetAutoRotation(false, true, false, false);
    }
    public static void SetPortraitAuto()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        SetAutoRotation(true, true, false, false);
    }
    public static void SetLandscapeLeft()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SetAutoRotation(false, false, true, false);
    }
    public static void SetLandscapeRight()
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        SetAutoRotation(false, false, false, true);
    }
    public static void SetLandscapeAuto()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        SetAutoRotation(false, false, true, true);
    }
    public static void FreeRotation()
    {
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
    }

    //Helper for all rotation methods
    private static void SetAutoRotation(bool portrait, bool portraitUpsideDown, bool landscapeLeft, bool landscapeRight)
    {
        Screen.autorotateToPortrait = portrait;
        Screen.autorotateToPortraitUpsideDown = portraitUpsideDown;
        Screen.autorotateToLandscapeLeft = landscapeLeft;
        Screen.autorotateToLandscapeRight = landscapeRight;

        if (portrait) Screen.orientation = ScreenOrientation.Portrait;
        else if (portraitUpsideDown) Screen.orientation = ScreenOrientation.PortraitUpsideDown;
        else if (landscapeLeft) Screen.orientation = ScreenOrientation.LandscapeLeft;
        else if (landscapeRight) Screen.orientation = ScreenOrientation.LandscapeRight;
    }
}

public class Array
{

    public static void PrintArray(int[,] array)
    {
        int rows = array.GetLength(0);
        int columns = array.GetLength(1);
        string matrix = ""; // To hold the entire matrix as a string

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                matrix += array[i, j] + " "; // Append each element with a space
            }
            matrix += "\n"; // Add a newline after each row
        }

        Debug.Log(matrix); // Log the matrix as a single string
    }


    public class Shift
    {
        public void LinearShift(int[,] array, int shiftRate)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            for (int shift = 0; shift < shiftRate; shift++)
            {
                for (int i = rows - 1; i > 0; i--)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        array[i, j] = array[i - 1, j];
                    }
                }
                for (int j = 0; j < columns; j++)
                {
                    array[0, j] = 0;
                }
            }
        }

    }

    public class Write
    {
        public void Custom(int[,] array, int row, int column, int value) {
            array[row, column] = value;
        }


        public void random(int[,] array, int MinValToFill, int MaxValToFill, int WhatToFill, int ByHowMuch)
        {
            if (WhatToFill == 0)
            {
                for (int i = 0; i < ByHowMuch; i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        array[i, j] = Random.Range(MinValToFill, MaxValToFill);
                    }
                }
            }
            if (WhatToFill == 1)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < ByHowMuch; j++)
                    {
                        array[i, j] = Random.Range(MinValToFill, MaxValToFill);
                    }
                }
            }
        }
    }

}

public class PlayerPrefsManager {
    // public static int score(int score, int increment) {
    //     return score += increment;
    // }
    public static int HighScorePlayerPrefs(int score, string PlayerPrefsKey, bool deletePlayerPrefs) {
        if (deletePlayerPrefs) {
            PlayerPrefs.DeleteKey(PlayerPrefsKey);
        }
        if (!PlayerPrefs.HasKey(PlayerPrefsKey)) {
            PlayerPrefs.SetInt(PlayerPrefsKey, 0);
            PlayerPrefs.Save();
        }
        if (score > PlayerPrefs.GetInt(PlayerPrefsKey)) {
            PlayerPrefs.SetInt(PlayerPrefsKey, score);
            PlayerPrefs.Save();
        }
        return PlayerPrefs.GetInt(PlayerPrefsKey);
    }
}
