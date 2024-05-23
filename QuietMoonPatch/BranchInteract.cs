using System;
using UnityEngine;

public class BranchInteract : MonoBehaviour
{
    public GameObject branch;
    public GameObject brokenBranch;

    private GameObject branchInstance;
    private GameObject brokenBranchInstance;

    private bool spawned = false;

    private float targetTime;

    private readonly Quaternion RAND_ROTATION = Quaternion.Euler(0f, (float)BranchRandom.Instance.random.NextDouble() * 360f, 0f);
    private static readonly Vector3 VEC_OFFSET = new Vector3(0, 0.08f, 0);

    private void Start()
    {
        targetTime = GetRandomTime();
        // Make sure collider is disabled
        GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!TryGetComponent<AudioSource>(out var audioData)) return;

        audioData.Play();
        RoundManager.Instance.PlayAudibleNoise(transform.position, noiseRange: 80f, noiseLoudness: 1f);

        GetComponent<Collider>().enabled = false;
        Destroy(branchInstance);
        brokenBranchInstance = Instantiate(brokenBranch, transform.position + VEC_OFFSET, RAND_ROTATION);
    }

    private void Update()
    {
        if (!spawned && TimeOfDay.Instance.normalizedTimeOfDay >= targetTime)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    Debug.Log("Player detected within 1 meter radius!");
                    return;
                }
            }

            branchInstance = Instantiate(branch, transform.position + VEC_OFFSET, RAND_ROTATION);
            GetComponent<Collider>().enabled = true;
            spawned = true;
        }
    }

    private void OnDestroy()
    {
        Destroy(branchInstance);
        Destroy(brokenBranchInstance);
    }

    private float GetRandomTime()
    {
        // Hardcoded inverse cumulative probability curve.
        // Branches become more likely to spawn later rather than sooner.
        return (float)Math.Sqrt(BranchRandom.Instance.random.NextDouble());
    }
}
