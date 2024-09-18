using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
    public GameObject gameOverPanel;


    void Start() {
        gameOverPanel.SetActive(false); 
        Time.timeScale = 1; 
    }

    public void ShowGameOver() {
        gameOverPanel.SetActive(true); 
        Time.timeScale = 0; 

    }

    public void RestartGame() {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}
