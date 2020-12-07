using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject m_player;
    [SerializeField] private int m_tilesOffset = 5;
    [SerializeField] private string m_floorLayer = "Floor";
    [SerializeField] private float m_scrollDuration = 0.5f;
    [SerializeField] private int m_floorTileTolerance = 1;
    [SerializeField] private float m_xSmoothTime = 0.5f;
    [SerializeField] private float m_xMaxSpeed = 0.5f;

    private int m_tileSize = 16;
    private int m_heightOffset => m_tilesOffset * m_tileSize;
    private int m_floorTileToleranceHeight => m_floorTileTolerance * m_tileSize;

    private float m_floorYPosition = float.MinValue;
    private float m_startScroll = 0f;
    private float m_velocity = 0f;

    private void Start()
    {
        LevelManager.Instance.OnRespawn += PlayerRespawn;
    }

    void Update()
    {
        // If floor position is not set or player is under current floor position
        if (m_floorYPosition == float.MinValue ||
            m_player.transform.position.y < (m_floorYPosition - m_floorTileToleranceHeight))
        {
            m_floorYPosition = GetFloor();
            m_startScroll = Time.time;
        }

        var t = Mathf.Clamp01((Time.time - m_startScroll) / m_scrollDuration);

        var x = Mathf.SmoothDamp(
            transform.position.x,
            m_player.transform.position.x,
            ref m_velocity,
            m_xSmoothTime,
            m_xMaxSpeed
        );

        transform.position = new Vector3(
            x,
            Mathf.Lerp(transform.position.y, m_floorYPosition + m_heightOffset, t),
            transform.position.z
        );
    }

    private float GetFloor()
    {
        var hit = Physics2D.Raycast(
            m_player.transform.position,
            transform.up * -1f,
            1000f,
            LayerMask.GetMask(m_floorLayer)
        );

        if (hit.collider != null)
        {
            return hit.point.y;
        }

        return float.MinValue;
    }

    private void PlayerRespawn(Transform obj)
    {
        m_floorYPosition = float.MinValue;
    }
}