using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer(collision))
        {
            FinishLevel();
        }
    }

    private int NextSceneIndex => SceneManager.GetActiveScene().buildIndex + 1;

    private bool IsPlayer(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponentInChildren<PlayerController>();

        return !!playerController;
    }

    private void FinishLevel()
    {
        enabled = false;

        SceneManager.LoadScene(NextSceneIndex);
    }
}
