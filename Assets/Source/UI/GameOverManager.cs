using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    [SerializeField] private InputListener _inputListener;

    private void Start()
	{
        GameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowGameOver()
	{
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
        _inputListener.Enabled = false;
    }

    public void RestartGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}