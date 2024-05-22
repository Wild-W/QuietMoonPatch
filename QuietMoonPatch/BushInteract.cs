using UnityEngine;

public class BushInteract : MonoBehaviour
{
    public AudioClip[] audioClips;

    private static readonly float shakeDuration = 0.3f;
    public static readonly float shakeMagnitude = 0.05f;

    private Vector3 originalPosition;
    private float shakeEndTime;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!TryGetComponent<AudioSource>(out var audioData)) return;

        audioData.clip = audioClips[BranchRandom.Instance.random.Next(audioClips.Length)];
        audioData.Play();
        RoundManager.Instance.PlayAudibleNoise(transform.position, noiseRange: 35f, noiseLoudness: 0.7f);
        shakeEndTime = Time.time + shakeDuration;
    }

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    // If there's a mod that moves game objects it won't work with my bushes
    // because they're hardcoded to return to their original position when touched
    void Update()
    {
        if (Time.time < shakeEndTime)
        {
            ShakeBush();
        }
        else
        {
            transform.localPosition = originalPosition;
        }
    }

    void ShakeBush()
    {
        Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;
        shakeOffset.y = 0; // Don't shake y axis

        transform.localPosition = originalPosition + shakeOffset;
    }
}