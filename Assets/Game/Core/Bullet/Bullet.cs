using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 16)] [SerializeField] private int m_delayFrames = 8;
    private int m_currentDelayFrames;

    // lol this is terrible code
    private Wall m_overlappingWall = null;
    private UnstableTile m_overlappingUnstableTile = null;

    [SerializeField] private GameObject m_unstableTilePrefab = null;

    public Vector2Int CurrentPosition => new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    public Vector2Int UpPosition => CurrentPosition + Vector2Int.up;
    public Vector2Int DownPosition => CurrentPosition + Vector2Int.down;
    public Vector2Int LeftPosition => CurrentPosition + Vector2Int.left;
    public Vector2Int RightPosition => CurrentPosition + Vector2Int.right;
    public Vector2Int ExtendedPosition => CurrentPosition + new Vector2Int(Mathf.RoundToInt(transform.up.x), Mathf.RoundToInt(transform.up.y)) * 2;

    private void Awake()
    {
        m_currentDelayFrames = m_delayFrames;
    }

    private void FixedUpdate()
    {
        while (m_currentDelayFrames > 0)
        {
            m_currentDelayFrames--;
        }

        if (OverlapsWall(CurrentPosition))
        {
            if (!m_overlappingWall.Impenetrable)
            {
                Destroy(m_overlappingWall.gameObject);

                DestroyTile(CurrentPosition);
                DestroyTile(UpPosition);
                DestroyTile(DownPosition);
                DestroyTile(LeftPosition);
                DestroyTile(RightPosition);
                DestroyTile(ExtendedPosition);
            }

            Destroy(gameObject);

            return;
        }

        Move();

        m_currentDelayFrames = m_delayFrames;
    }

    private void Move()
    {
        transform.position += transform.up;
    }

    private bool OverlapsWall(Vector2Int pos)
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(pos, Vector2.one * 0.5f, 0);

        for (int i = 0; i < cols.Length; ++i)
        {
            Collider2D col = cols[i];

            Wall wall = col.GetComponentInChildren<Wall>();

            if (!!wall)
            {
                m_overlappingWall = wall;
                return true;
            }
        }

        return false;
    }

    private bool OverlapsUnstableTile(Vector2Int pos)
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(pos, Vector2.one * 0.5f, 0);

        for (int i = 0; i < cols.Length; ++i)
        {
            Collider2D col = cols[i];

            UnstableTile tile = col.GetComponentInChildren<UnstableTile>();

            if (!!tile)
            {
                m_overlappingUnstableTile = tile;
                return true;
            }
        }

        return false;
    }

    private bool OverlapsTrapTile(Vector2Int pos)
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(pos, Vector2.one * 0.5f, 0);

        for (int i = 0; i < cols.Length; ++i)
        {
            Collider2D col = cols[i];

            TrapTile tile = col.GetComponentInChildren<TrapTile>();

            if (!!tile)
            {
                return true;
            }
        }

        return false;
    }

    private void DestroyTile(Vector2Int pos)
    {
        if (OverlapsWall(pos) || OverlapsTrapTile(pos))
            return;

        if (OverlapsUnstableTile(pos))
        {
            m_overlappingUnstableTile.Collapse();
            return;
        }

        Instantiate(m_unstableTilePrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
    }
}
