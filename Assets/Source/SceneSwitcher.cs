using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void Switch(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}