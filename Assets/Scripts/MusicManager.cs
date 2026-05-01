using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Tracks")]
    [SerializeField] private AudioClip trackA;
    [SerializeField] private AudioClip trackB;

    private AudioSource _audioSource;
    private bool _playingA = true;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
        PlayCurrent();
    }

    void Update()
    {
        if (!_audioSource.isPlaying)
        {
            _playingA = !_playingA;
            PlayCurrent();
        }
    }

    private void PlayCurrent()
    {
        _audioSource.clip = _playingA ? trackA : trackB;
        _audioSource.Play();
    }
}