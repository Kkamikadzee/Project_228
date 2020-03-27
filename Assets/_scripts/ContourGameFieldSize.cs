using UnityEngine;
using UnityEngine.UI;

public class ContourGameFieldSize : MonoBehaviour
{
    public GameObject Field;
    public GameObject swipeControls;

    public int constant = 32;

	void OnEnable()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(swipeControls.GetComponent<Game>().Width * Field.GetComponent<GridLayoutGroup>().cellSize.x + constant, swipeControls.GetComponent<Game>().Height * Field.GetComponent<GridLayoutGroup>().cellSize.x + constant);
	}

}
