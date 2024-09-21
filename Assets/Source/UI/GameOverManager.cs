using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;

    private void Awake()
	{
        GameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
	{
        GameOverPanel.SetActive(true);
    }

    public void RestartGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}