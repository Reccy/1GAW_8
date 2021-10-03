using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTile : MonoBehaviour
{
    private PlayerController m_playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsPlayer(collision))
            return;

        m_playerController.Kill();
    }

    private bool IsPlayer(Collider2D collision)
    {
        m_playerController = collision.GetComponentInChildren<PlayerController>();

        return !!m_playerController;
    }
}
