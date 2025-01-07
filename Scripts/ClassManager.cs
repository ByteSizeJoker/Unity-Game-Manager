using UnityEngine;

//# Formatted and Commented

/// <summary>
/// A class for managing global game states.
/// </summary>
public class ClassManager
{
    public static bool FB_restarted = false;  // Tracks whether the game has been restarted.
}

/// <summary>
///  A Screen Orientation Managing class for controlling screen orientation
/// </summary>
public class ScreenOrientationManager
{
    /// <summary>
    /// Sets the screen orientation to Portrait mode.
    /// </summary>
    public static void SetPortrait()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SetAutoRotation(true, false, false, false);
    }

    /// <summary>
    /// Sets the screen orientation to Portrait Upside Down.
    /// </summary>
    public static void SetPortraitUpsideDown()
    {
        Screen.orientation = ScreenOrientation.PortraitUpsideDown;
        SetAutoRotation(false, true, false, false);
    }

    /// <summary>
    /// Sets the screen orientation to AutoRotation with Portrait mode enabled.
    /// </summary>
    public static void SetPortraitAuto()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        SetAutoRotation(true, true, false, false);
    }

    /// <summary>
    /// Sets the screen orientation to Landscape Left.
    /// </summary>
    public static void SetLandscapeLeft()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SetAutoRotation(false, false, true, false);
    }

    /// <summary>
    /// Sets the screen orientation to Landscape Right.
    /// </summary>
    public static void SetLandscapeRight()
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        SetAutoRotation(false, false, false, true);
    }

    /// <summary>
    /// Sets the screen orientation to AutoRotation with Landscape mode enabled.
    /// </summary>
    public static void SetLandscapeAuto()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        SetAutoRotation(false, false, true, true);
    }

    /// <summary>
    /// Allows free rotation by enabling all rotation directions.
    /// </summary>
    public static void FreeRotation()
    {
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
    }

    /// <summary>
    /// Helper method to set automatic rotation settings based on the given parameters.
    /// </summary>
    /// <param name="portrait">Allow Portrait rotation</param>
    /// <param name="portraitUpsideDown">Allow Portrait UpsideDown rotation</param>
    /// <param name="landscapeLeft">Allow Landscape Left rotation</param>
    /// <param name="landscapeRight">Allow Landscape Right rotation</param>
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

/// <summary>
/// A helper class to manipulate 2D arrays with various functions like printing, shifting, and random filling.
/// </summary>
public class Array
{
    /// <summary>
    /// Prints the contents of a 2D array as a formatted string to the console.
    /// </summary>
    /// <param name="array">The 2D array to be printed.</param>
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

    /// <summary>
    /// A class to handle shifting of 2D arrays.
    /// </summary>
    public class Shift
    {
        /// <summary>
        /// Shifts rows of a 2D array by a given rate.
        /// </summary>
        /// <param name="array">The array to shift.</param>
        /// <param name="shiftRate">The number of times to shift the array.</param>
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

    /// <summary>
    /// A class for writing custom and random values into a 2D array.
    /// </summary>
    public class Write
    {
        /// <summary>
        /// Writes a custom value to a specified position in the array.
        /// </summary>
        /// <param name="array">The array to modify.</param>
        /// <param name="row">The row to modify.</param>
        /// <param name="column">The column to modify.</param>
        /// <param name="value">The value to write into the array.</param>
        public void Custom(int[,] array, int row, int column, int value)
        {
            array[row, column] = value;
        }

        /// <summary>
        /// Fills the array with random values within a given range.
        /// </summary>
        /// <param name="array">The array to fill.</param>
        /// <param name="MinValToFill">Minimum value for random number generation.</param>
        /// <param name="MaxValToFill">Maximum value for random number generation.</param>
        /// <param name="WhatToFill">Specify which dimension of the array to fill (0 for rows, 1 for columns).</param>
        /// <param name="ByHowMuch">Specifies how many rows/columns to fill.</param>
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

/// <summary>
/// Manages the player's preferences, specifically high score tracking.
/// </summary>
public class PlayerPrefsManager
{
    /// <summary>
    /// Updates or retrieves the high score stored in PlayerPrefs.
    /// If the score is higher than the stored high score, it is updated.
    /// </summary>
    /// <param name="score">The current score to compare with the stored high score.</param>
    /// <param name="PlayerPrefsKey">The key used to retrieve or store the high score.</param>
    /// <param name="deletePlayerPrefs">If true, the stored high score is deleted before checking.</param>
    /// <returns>The highest score retrieved from PlayerPrefs.</returns>
    public static int HighScorePlayerPrefs(int score, string PlayerPrefsKey, bool deletePlayerPrefs)
    {
        if (deletePlayerPrefs)
        {
            PlayerPrefs.DeleteKey(PlayerPrefsKey);
        }
        if (!PlayerPrefs.HasKey(PlayerPrefsKey))
        {
            PlayerPrefs.SetInt(PlayerPrefsKey, 0);
            PlayerPrefs.Save();
        }
        if (score > PlayerPrefs.GetInt(PlayerPrefsKey))
        {
            PlayerPrefs.SetInt(PlayerPrefsKey, score);
            PlayerPrefs.Save();
        }
        return PlayerPrefs.GetInt(PlayerPrefsKey);
    }
}