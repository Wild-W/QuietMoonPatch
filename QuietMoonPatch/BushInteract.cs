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
        RoundManager.Instance.PlayAudibleNoise(transform.position, noiseRange: 25f, noiseLoudness: 0.6f);
        shakeEndTime = Time.time + shakeDuration;
    }

    void Start()
    {
        originalPosition = transform.localPosition;
    }

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