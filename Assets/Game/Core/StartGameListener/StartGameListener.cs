using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class StartGameListener : MonoBehaviour
{
    private const int PLAYER_ID = 0;
    private Player m_player;

    [SerializeField] private SceneLoader m_loader;

    private void Awake()
    {
        m_player = ReInput.players.GetPlayer(PLAYER_ID);
    }

    private void Update()
    {
        if (m_player.GetButtonDown("BeginGame"))
        {
            m_loader.LoadScene();
            Destroy(gameObject);
        }
    }
}
