using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    private PlayerController m_pc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsPlayer(collision))
            return;

        m_pc.Ammo += 1;
        Destroy(gameObject);
    }

    private bool IsPlayer(Collider2D collision)
    {
        m_pc = collision.GetComponentInChildren<PlayerController>();

        return !!m_pc;
    }
}
