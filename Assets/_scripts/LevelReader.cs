using System;
using System.IO;
using UnityEngine;

public class LevelReader : MonoBehaviour
{
    public static int numberOfLevel = 30;
    public static LevelList levelList = new LevelList(numberOfLevel);

    private string json;
    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, "Levels/" + "levels.json");
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        json = reader.text;
#endif
#if UNITY_EDITOR
        json = File.ReadAllText(Application.streamingAssetsPath + "/Levels/" +  "levels.json");
#endif

        #region For New Levels
        //ReaderForSave(@"M:\•Test game\Project 228\Project 228\Assets\_levels\Levels.csv");
        //SaveJson();
        #endregion

        LoadLevels();
    }
    private void SaveJson()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/Levels/" + "levels.json", JsonUtility.ToJson(levelList));
    }
    private void ReaderForSave(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string lines = reader.ReadLine();
            string[] values = lines.Split(';');
            levelList.NumberOfLevel = Convert.ToInt32(values[0]);

            int counter = 0;
            while(!reader.EndOfStream)
            {
                lines = reader.ReadLine();
                values = lines.Split(';');
                levelList.level[counter] = new Level();
                levelList.level[counter].height = Convert.ToInt32(values[0]);
                levelList.level[counter].width = Convert.ToInt32(values[1]);
                levelList.level[counter].column = new Level.Line[levelList.level[counter].height];
                for (int i = 0; i < levelList.level[counter].height; i++)
                {
                    lines = reader.ReadLine();
                    values = lines.Split(';');
                    levelList.level[counter].column[i] = new Level.Line(levelList.level[counter].width);
                    for (int j = 0; j < levelList.level[counter].width; j++)
                    {
                        levelList.level[counter].column[i].row[j] = Convert.ToInt32(values[j]);
                        switch(levelList.level[counter].column[i].row[j])
                        {
                            case 0:
                                levelList.level[counter].emptyCells++;
                                break;
                            case 2:
                                levelList.level[counter].emptyCells++;
                                levelList.level[counter].playerPositionX = j;
                                levelList.level[counter].playerPositionY = i;
                                break;
                        }
                    }
                }
                lines = reader.ReadLine();
                values = lines.Split(';');
                if (values[0] == "#")
                    counter++;
            }
            levelList.NumberOfLevel = counter;
        }
    }

    private void LoadLevels()
    {
        levelList = JsonUtility.FromJson<LevelList>(json);
    }
}
[Serializable]
public class Level
{
    public int width, height;
    [Serializable]
    public class Line
    {
        public int[] row;
        public Line(int size)
        {
            row = new int[size];
        }
    }
    public Line[] column;
    public int emptyCells = -1;
    public int playerPositionX, playerPositionY;

    public Level() { }
    public Level(int w, int h, int[,] array)
    {
        width = w;
        height = h;
        column = new Line[h];
        for(int i = 0; i< height; i++)
        {
            column[i] = new Line(width);
            for(int j = 0; j< width; j++)
            {
                column[i].row[j] = array[i, j];
            }
        }
    }
    public int[,] ToIntArray()
    {
        int[,] array = new int[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                array[i, j] = column[i].row[j];
            }
        }
        return array;
    }
}
[Serializable]
public class LevelList
{
    public int NumberOfLevel;
    public Level[] level;
    public LevelList() { }
    public LevelList(int number)
    {
        NumberOfLevel = number;
        level = new Level[NumberOfLevel];
    }
}
