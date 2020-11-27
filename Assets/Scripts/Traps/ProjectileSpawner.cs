using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    Projectile ProjectilePrefab;

    [SerializeField]
    Vector2 ProjectileDirection;

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
            Instantiate(ProjectilePrefab, transform.position, Quaternion.identity );
            if(ProjectileDirection == Vector2.zero)
                ProjectilePrefab.DirProjectile = (Random.insideUnitCircle * 2 - Vector2.one).normalized;
            else
                ProjectilePrefab.DirProjectile = ProjectileDirection;
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }
}
