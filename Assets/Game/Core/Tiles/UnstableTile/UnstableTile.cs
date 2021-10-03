using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableTile : MonoBehaviour
{
    [SerializeField] private GameObject m_trapTilePrefab;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!IsPlayer(collision))
            return;

        Collapse();
    }

    public void Collapse()
    {
        Instantiate(m_trapTilePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private bool IsPlayer(Collider2D collision)
    {
        return collision.GetComponentInChildren<PlayerController>();
    }
}
