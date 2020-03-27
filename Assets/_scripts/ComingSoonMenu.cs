using UnityEngine;

public class ComingSoonMenu : MonoBehaviour
{
    public GameObject TextSwipe;

    private void OnEnable()
    {
        TextSwipe.SetActive(false);
    }

    private void OnDisable()
    {
        TextSwipe.SetActive(true);
    }
}
