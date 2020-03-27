using System;
using UnityEngine;
using UnityEngine.UI;

public class Initialization : MonoBehaviour
{
    public SwipeControls swipeControls;
    public GameObject LevelPassedMenu;

    public GameObject[] Scenes = new GameObject[4];

    private Save save = new Save();
    private SettingsSave settingsSave = new SettingsSave();

    public AudioSource musicSource;
    public AudioSource soundSource;

    #region Text
    [Header("Localization")]
    public Text TextSwipeStart;
    public Text TextTotalScore;
    public Text TextSwipeStartGame;
    public Text TextScoreGame;
    public Text TextComingSoon;
    public Text TextStartMenuVer;
    public Text TextPassedLevel;
    public Text TextRestartVer;
    public Text TextSwipeSettings;
    public Text TextResetProgress;
    public Text TextResetVer;
    public Text TextHelp;
    public Text TextSwipeHelp;
    #endregion

    private void Start ()
    {
        for (int i = 0; i < Scenes.Length; i++)
            Scenes[i].SetActive(false);
        swipeControls.SetStatus(0);
        LevelPassedMenu.SetActive(false);
        LoadLanguage();
        LoadSettings();
        LoadProgress();
    }

    #region Saves
    public void LoadProgress()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            save = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));
            swipeControls.Level = save.Level;
            swipeControls.TotalScrore = save.TotalScore;
        }
        else
        {
            swipeControls.Level = 0;
            swipeControls.TotalScrore = 0;
        }
    }
    public void SaveProgress(int level, int totalScore)
    {
        save.TotalScore = totalScore;
        save.Level = level;
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(save));
    }
    public void ResetProgress()
    {
        save.Level = 0;
        save.TotalScore = 0;
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(save));
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("SettingsSave"))
        {
            settingsSave = JsonUtility.FromJson<SettingsSave>(PlayerPrefs.GetString("SettingsSave"));
            musicSource.volume = settingsSave.MusicVolume;
            soundSource.volume = settingsSave.SoundVolume;
        }
    }
    public void SaveSettings(float soundVolume, float musicVolume)
    {
        settingsSave.SoundVolume = soundVolume;
        settingsSave.MusicVolume = musicVolume;
        PlayerPrefs.SetString("SettingsSave", JsonUtility.ToJson(settingsSave));
    }
    public void ResetSettings()
    {
        settingsSave.SoundVolume = 1;
        settingsSave.MusicVolume = 1;
        PlayerPrefs.SetString("SettingsSave", JsonUtility.ToJson(settingsSave));
    }
    #endregion

    public void LoadLanguage()
    {
        TextSwipeStart.text = LangSystem.lng.SwipeToStart;
        TextTotalScore.text = LangSystem.lng.TotalScore;
        TextSwipeStartGame.text = LangSystem.lng.SwipeToStart;
        TextScoreGame.text = LangSystem.lng.Score;
        TextComingSoon.text = LangSystem.lng.ComingSoon;
        TextStartMenuVer.text = LangSystem.lng.StartMenuVer;
        TextPassedLevel.text = LangSystem.lng.PassedLevel;
        TextRestartVer.text = LangSystem.lng.RestartVer;
        TextSwipeSettings.text = LangSystem.lng.SwipeToComeBack;
        TextResetProgress.text = LangSystem.lng.ResetProgress;
        TextResetVer.text = LangSystem.lng.ResetVer;
        TextHelp.text = LangSystem.lng.Help;
        TextSwipeHelp.text = LangSystem.lng.SwipeToComeBack;
    }
}

[Serializable]
public class Save
{
    public int Level;
    public int TotalScore;
}
[Serializable]
public class SettingsSave
{
    public float SoundVolume;
    public float MusicVolume;
}