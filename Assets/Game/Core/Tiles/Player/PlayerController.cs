using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    #region INPUT
    private bool m_inputMoveUp = false;
    private bool m_inputMoveDown = false;
    private bool m_inputMoveLeft = false;
    private bool m_inputMoveRight = false;
    #endregion

    private const int PLAYER_ID = 0;
    private Player m_player;

    private void Awake()
    {
        m_player = ReInput.players.GetPlayer(PLAYER_ID);
    }

    private void Update()
    {
        // Short circuit bools to unset them in fixedupdate
        m_inputMoveUp = m_inputMoveUp || m_player.GetButtonDown("MoveUp");
        m_inputMoveDown = m_inputMoveDown || m_player.GetButtonDown("MoveDown");
        m_inputMoveLeft = m_inputMoveLeft || m_player.GetButtonDown("MoveLeft");
        m_inputMoveRight = m_inputMoveRight || m_player.GetButtonDown("MoveRight");
    }

    private void FixedUpdate()
    {
        // Movement
        if (m_inputMoveUp)
        {
            transform.position += Vector3.up;
            m_inputMoveUp = false;
        }

        if (m_inputMoveDown)
        {
            transform.position += Vector3.down;
            m_inputMoveDown = false;
        }

        if (m_inputMoveLeft)
        {
            transform.position += Vector3.left;
            m_inputMoveLeft = false;
        }

        if (m_inputMoveRight)
        {
            transform.position += Vector3.right;
            m_inputMoveRight = false;
        }
    }
}
