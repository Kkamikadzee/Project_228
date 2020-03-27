using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class HintController : MonoBehaviour
{
    public static Stack StepX = new Stack(), StepY = new Stack();

    private string json;
    public static PassedLevels Hint = new PassedLevels(LevelReader.numberOfLevel);

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath,"Levels/" + "passedLevels.json");
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        json = reader.text;
#endif
#if UNITY_EDITOR
        json = File.ReadAllText(Application.streamingAssetsPath + "/Levels/" + "passedLevels.json");
#endif

        #region For New Levels
        //ReaderForSave(@"M:\•Test game\Project 228\Project 228\Assets\_levels\PassedLevels.csv");
        //SaveJson();
        #endregion

        LoadPassedLevels();
    }

    private void LoadPassedLevels()
    {
        Hint = JsonUtility.FromJson<PassedLevels>(json);
    }

    private void SaveJson()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/Levels/" + "passedLevels.json", JsonUtility.ToJson(Hint));
    }
    private void ReaderForSave(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string lines = reader.ReadLine();
            string[] values = lines.Split(';');
            Hint.NumberOfLevel = Convert.ToInt32(values[0]);

            int counter = 0;
            while (!reader.EndOfStream)
            {
                lines = reader.ReadLine();
                values = lines.Split(';');
                Hint.level[counter] = new PassedLevel(Convert.ToInt32(values[0]));
                lines = reader.ReadLine();
                values = lines.Split(';');
                for (int i = 0; i < Hint.level[counter].NumberOfStep; i++)
                {
                    switch(values[i])
                    {
                        case "a":
                            Hint.level[counter].direction[i] = 1;
                            break;
                        case "d":
                            Hint.level[counter].direction[i] = 2;
                            break;
                        case "w":
                            Hint.level[counter].direction[i] = 3;
                            break;
                        case "s":
                            Hint.level[counter].direction[i] = 4;
                            break;
                    }
                }
                lines = reader.ReadLine();
                values = lines.Split(';');
                if (values[0] == "#")
                    counter++;
            }
        }
    }

    public static void ClearStackStep()
    {
        StepX.Clear();
        StepY.Clear();
    }
}
[Serializable]
public class PassedLevel
{
    public int NumberOfStep;
    public int[] direction;

    PassedLevel() { }
    public PassedLevel(int number)
    {
        NumberOfStep = number;
        direction = new int[number];
    }
}
[Serializable]
public class PassedLevels
{
    public int NumberOfLevel;
    public PassedLevel[] level;
    
    PassedLevels() { }
    public PassedLevels(int number)
    {
        NumberOfLevel = number;
        level = new PassedLevel[number];
    }
}