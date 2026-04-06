using UnityEngine;

public class graincounter : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private AudioSource _whiteNOise;
    PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
    }

    private void Update()
    {
        _material.SetFloat("_grain",0.9f - (0.003f * playerStats.stress));
        _whiteNOise.volume = playerStats.stress * 0.01f;
    }
}
