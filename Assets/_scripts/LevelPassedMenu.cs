using UnityEngine;
using UnityEngine.UI;

public class LevelPassedMenu : MonoBehaviour
{
    public GameObject GOLevelPassedMenu;

    public GameObject Buttons;
    public GameObject TextScore;

	private void OnEnable()
    {
        Buttons.SetActive(false);
        this.transform.Find("TextScore").GetComponent<Text>().text = TextScore.GetComponent<Text>().text;
        TextScore.SetActive(false);
	}

    private void OnDisable()
    {
        Buttons.SetActive(true);
        TextScore.SetActive(true);
    }

    public void CloseLevelPassedMenu()
    {
        GOLevelPassedMenu.SetActive(false);
    }
}
