using UnityEngine;

public class PauseManager : MonoBehaviour
{
	public GameObject PausePanel;

	private void Start()
	{
        PausePanel.SetActive(false);
    }

    public void Show()
	{
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Hide()
	{
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }
}