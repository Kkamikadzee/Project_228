using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LangSystem : MonoBehaviour
{
    public Image langButtonImg;
    public Sprite[] flags;

    private string json;
    public static Lang lng = new Lang();
    private int langIndex = 1;
    private string[] langArray = { "ru_RU", "en_US", "de_DE", "uk_UA" };


    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                    PlayerPrefs.SetString("Language", "ru_RU");
                    break;
                case SystemLanguage.Ukrainian:
                    PlayerPrefs.SetString("Language", "uk_UA");
                    break;
                case SystemLanguage.German:
                    PlayerPrefs.SetString("Language", "de_DE");
                    break;
                default:
                    PlayerPrefs.SetString("Language", "en_US");
                    break;
            }
        }
        LoadLang();
    }

    private void Start()
    {
        for(int i = 0; i<langArray.Length; i++)
        {
            if(PlayerPrefs.GetString("Language") == langArray[i])
            {
                langIndex = i + 1;
                langButtonImg.sprite = flags[langIndex - 1];
                break;
            }
        }
    }

    private void LoadLang()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, "Languages/" + PlayerPrefs.GetString("Language") + ".json");
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        json = reader.text;
#endif
#if UNITY_EDITOR
        json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Language") + ".json");
#endif
        lng = JsonUtility.FromJson<Lang>(json);
    }

    public void SwitchLangButton()
    {
        if(langIndex != langArray.Length)
            langIndex++;
        else
            langIndex = 1;
        PlayerPrefs.SetString("Language", langArray[langIndex - 1]);
        langButtonImg.sprite = flags[langIndex - 1];
        LoadLang();
        GetComponent<Initialization>().LoadLanguage();
    }
}
public class Lang
{
    public string SwipeToStart;
    public string TotalScore;
    public string Score;
    public string ComingSoon;
    public string StartMenuVer;
    public string PassedLevel;
    public string RestartVer;
    public string SwipeToComeBack;
    public string ResetProgress;
    public string ResetVer;
    public string Help;
}