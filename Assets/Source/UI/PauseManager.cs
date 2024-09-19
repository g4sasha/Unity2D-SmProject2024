using UnityEngine;

public class PauseManager : MonoBehaviour
{
	public GameObject PausePanel;

	private void Start()
	{
        PausePanel.SetActive(false);
    }

    private void Update()
    {
        if (PausePanel.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Show()
	{
        PausePanel.SetActive(true);
    }

    public void Hide()
	{
        PausePanel.SetActive(false);
    }
}