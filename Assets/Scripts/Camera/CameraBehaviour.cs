using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehaviour : MonoBehaviour
{
    public static CameraBehaviour Instance;

    [SerializeField] private GameObject m_player;
    [SerializeField] private int m_tilesOffset = 5;
    [SerializeField] private string m_floorLayer = "Floor";
    [SerializeField] private float m_scrollDuration = 0.5f;
    [SerializeField] private int m_floorTileTolerance = 1;
    [SerializeField] private float m_xSmoothTime = 0.5f;
    [SerializeField] private float m_xMaxSpeed = 0.5f;

    private bool m_initialized = false;

    private int m_tileSize = 16;
    private int m_heightOffset => m_tilesOffset * m_tileSize;
    private int m_floorTileToleranceHeight => m_floorTileTolerance * m_tileSize;

    private float m_floorYPosition = float.MinValue;
    private float m_startScroll = 0f;
    private float m_velocity = 0f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (m_initialized)
            return;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.sceneUnloaded += SceneUnloaded;
    }

    private void SceneLoaded(Scene p_scene, LoadSceneMode p_sceneMode)
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnRespawn += PlayerRespawn;
        }

        if (PlayerInstance.Instance != null)
        {
            m_player = PlayerInstance.Instance.gameObject;
            StartCoroutine(_GetFloor());
        }
    }

    private void SceneUnloaded(Scene arg0)
    {

    }

    void LateUpdate()
    {
        if (m_player != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
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
            m_player.transform.position + transform.up,
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

    private IEnumerator _GetFloor()
    {
        yield return null;
        m_floorYPosition = GetFloor();
    }

    private void PlayerRespawn(Transform obj)
    {
        m_floorYPosition = float.MinValue;
    }
}