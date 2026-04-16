using System.Collections;
using UnityEngine;

public class CoinPayout : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform spawnPoint;

    public float launchForceMin = 4.5f;
    public float launchForceMax = 7f;
    public float upwardForceMin = 2f;
    public float upwardForceMax = 3.5f;
    public float sideSpread = 0.8f;
    public float spawnInterval = 0.04f;

    public void Payout(int count)
    {
        StartCoroutine(PayoutRoutine(count));
    }

    IEnumerator PayoutRoutine(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnCoin();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCoin()
    {
        if (coinPrefab == null || spawnPoint == null) return;

        GameObject coin = Instantiate(coinPrefab, spawnPoint.position, Random.rotation);

        Rigidbody rb = coin.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 dir = spawnPoint.forward;
            dir += spawnPoint.right * Random.Range(-sideSpread, sideSpread);
            dir += spawnPoint.up * Random.Range(upwardForceMin, upwardForceMax);
            dir.Normalize();

            float force = Random.Range(launchForceMin, launchForceMax);
            rb.AddForce(dir * force, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * 8f, ForceMode.Impulse);
        }
    }
}