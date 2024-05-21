using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchInteract : MonoBehaviour
{
    public GameObject branch;
    public GameObject brokenBranch;

    private readonly Quaternion RAND_ROTATION = Quaternion.Euler(0f, (float)BranchRandom.Instance.random.NextDouble() * 360f, 0f);
    private static readonly Vector3 VEC_OFFSET = new Vector3(0, 0.08f, 0);

    private void Start()
    {
        branch = Instantiate(branch, transform.position + VEC_OFFSET, RAND_ROTATION);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!TryGetComponent<AudioSource>(out var audioData)) return;

        audioData.Play();
        RoundManager.Instance.PlayAudibleNoise(transform.position, noiseRange: 35f, noiseLoudness: 1f);

        GetComponent<Collider>().enabled = false;
        Destroy(branch);
        Instantiate(brokenBranch, transform.position + VEC_OFFSET, RAND_ROTATION);
    }
}
