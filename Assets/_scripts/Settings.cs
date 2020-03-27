using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject RestartMenu;

    public AudioController audioController;

    private void OnEnable()
    {
        audioController.LoadScrollbars();
        if (RestartMenu.activeSelf)
            RestartMenu.SetActive(false);
    }

    private void OnDisable()
    {
        if (RestartMenu.activeSelf)
            RestartMenu.SetActive(false);
    }
}
