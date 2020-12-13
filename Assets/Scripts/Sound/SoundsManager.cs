using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;

    [SerializeField] private GameObject m_oneShotSFXPrefab;

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

    public void PlayOneShot(SFX p_sfx)
    {
        var oneShotSfx = SimplePool.Spawn(m_oneShotSFXPrefab, Vector3.zero, Quaternion.identity);
        oneShotSfx.GetComponent<OneShotSFX>().Play(p_sfx);
    }

    public void PlaySound(SFX p_sfx, float p_duration)
    {
        // Duration Clip   = Pitch (1)
        // Wanted duration = Pitch (?)
    }
}