using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;

    [SerializeField] private GameObject m_oneShotSFXPrefab;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    public void PlayOneShot(SFX p_sfx)
    {
        var oneShotSfx = SimplePool.Spawn(m_oneShotSFXPrefab, Vector3.zero, Quaternion.identity);
        oneShotSfx.GetComponent<OneShotSFX>().Play(p_sfx);
    }
}