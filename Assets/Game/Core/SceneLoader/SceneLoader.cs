using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private int m_sceneIndex;

    [SerializeField]
    private bool m_restartScene;

    public void LoadScene()
    {
        if (m_restartScene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(m_sceneIndex);
        }
    }
}
