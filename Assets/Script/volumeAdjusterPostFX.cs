using UnityEngine;
using UnityEngine.Rendering;

public class volumeAdjusterPostFX : MonoBehaviour
{
    private Volume _volume;
    private PlayerStats _playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerStats = FindFirstObjectByType<PlayerStats>();
        _volume = GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        _volume.weight = _playerStats.stress * 0.01f;
    }
}
