using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f; // 延迟爆炸时间
    public float explosionForce = 700f; // 爆炸力
    public float explosionRadius = 5f; // 爆炸半径
    public GameObject explosionEffect; // 可选，粒子特效

    private bool hasExploded = false;

    void Start()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        // 显示爆炸特效
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // 获取爆炸范围内所有刚体
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        Destroy(gameObject); // 删除手雷
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}