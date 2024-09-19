using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    [SerializeField] private InputListener _inputListener;

    private void Start()
	{
        GameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (GameOverPanel.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ShowGameOver()
	{
        GameOverPanel.SetActive(true);
        _inputListener.Enabled = false;
    }

    public void RestartGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}