using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    Projectile ProjectilePrefab;

    [SerializeField]
    float SpawnCooldown = 1.0f;
    void Start()
    {
        StartCoroutine(FireProjectile());
    }

    // Update is called once per frame
    IEnumerator FireProjectile()
    {
        while( true )
        {
            Instantiate(ProjectilePrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(SpawnCooldown * 1.0f);
        }
    }
}
