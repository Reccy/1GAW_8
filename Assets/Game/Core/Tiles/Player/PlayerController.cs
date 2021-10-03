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

    private bool m_inputShootUp = false;
    private bool m_inputShootDown = false;
    private bool m_inputShootLeft = false;
    private bool m_inputShootRight = false;
    #endregion

    private const int PLAYER_ID = 0;
    private Player m_player;

    private Vector3Int m_currentTilePos;
    public Vector3Int CurrentTilePos => m_currentTilePos;
    public Vector3Int UpTilePos => m_currentTilePos + Vector3Int.up;
    public Vector3Int DownTilePos => m_currentTilePos + Vector3Int.down;
    public Vector3Int LeftTilePos => m_currentTilePos + Vector3Int.left;
    public Vector3Int RightTilePos => m_currentTilePos + Vector3Int.right;

    private bool m_dead = false;
    public bool IsDead => m_dead;

    [SerializeField] private GameObject m_bulletPrefab;

    private void Awake()
    {
        m_player = ReInput.players.GetPlayer(PLAYER_ID);
        UpdateCurrentTilePos();
    }

    private void Update()
    {
        // Short circuit bools to unset them in fixedupdate
        m_inputMoveUp = m_inputMoveUp || m_player.GetButtonDown("MoveUp");
        m_inputMoveDown = m_inputMoveDown || m_player.GetButtonDown("MoveDown");
        m_inputMoveLeft = m_inputMoveLeft || m_player.GetButtonDown("MoveLeft");
        m_inputMoveRight = m_inputMoveRight || m_player.GetButtonDown("MoveRight");

        m_inputShootUp = m_inputShootUp || m_player.GetButtonDown("ShootUp");
        m_inputShootDown = m_inputShootDown || m_player.GetButtonDown("ShootDown");
        m_inputShootLeft = m_inputShootLeft || m_player.GetButtonDown("ShootLeft");
        m_inputShootRight = m_inputShootRight || m_player.GetButtonDown("ShootRight");
    }

    private void FixedUpdate()
    {
        if (IsDead)
            return;

        Shoot();
        Move();
        ClearInputFlags();
    }

    public void Kill()
    {
        m_dead = true;
    }

    private bool TileIsWalkable(Vector3Int tilePos)
    {
        Vector2Int pos = new Vector2Int(tilePos.x, tilePos.y);
        Collider2D[] cols = Physics2D.OverlapBoxAll(pos, Vector2.one * 0.5f, 0);

        for(int i = 0; i < cols.Length; ++i)
        {
            Collider2D col = cols[i];

            if (col.GetComponentInChildren<Wall>())
            {
                return false;
            }
        }

        return true;
    }

    private void Shoot()
    {
        if (m_inputShootUp)
        {
            Instantiate(m_bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        }
        else if (m_inputShootDown)
        {
            Instantiate(m_bulletPrefab, transform.position, Quaternion.Euler(0, 0, 180));
        }
        else if (m_inputShootLeft)
        {
            Instantiate(m_bulletPrefab, transform.position, Quaternion.Euler(0, 0, 90));
        }
        else if (m_inputShootRight)
        {
            Instantiate(m_bulletPrefab, transform.position, Quaternion.Euler(0, 0, 270));
        }
    }

    private void Move()
    {
        if (m_inputMoveUp && TileIsWalkable(UpTilePos))
        {
            transform.position += Vector3.up;
        }
        else if (m_inputMoveDown && TileIsWalkable(DownTilePos))
        {
            transform.position += Vector3.down;
        }
        else if (m_inputMoveLeft && TileIsWalkable(LeftTilePos))
        {
            transform.position += Vector3.left;
        }
        else if (m_inputMoveRight && TileIsWalkable(RightTilePos))
        {
            transform.position += Vector3.right;
        }

        UpdateCurrentTilePos();
    }
    
    private void UpdateCurrentTilePos()
    {
        m_currentTilePos = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
    }

    private void ClearInputFlags()
    {
        m_inputMoveUp = false;
        m_inputMoveDown = false;
        m_inputMoveLeft = false;
        m_inputMoveRight = false;

        m_inputShootUp = false;
        m_inputShootDown = false;
        m_inputShootLeft = false;
        m_inputShootRight = false;
    }
}
