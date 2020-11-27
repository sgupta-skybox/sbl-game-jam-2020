using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    Projectile ProjectilePrefab;

    [SerializeField]
    Vector2 ProjectileDirection;

    Vector2 nextProjectileDirection;

    [SerializeField]
    float SpawnCooldown = 1.0f;
    void Start()
    {
        nextProjectileDirection = ProjectileDirection;
        StartCoroutine(FireProjectile());
    }

    // Update is called once per frame
    IEnumerator FireProjectile()
    {
        while( true )
        {
            if (ProjectileDirection == Vector2.zero)
                nextProjectileDirection = Random.insideUnitCircle.normalized;
            else
                nextProjectileDirection = ProjectileDirection;

            float angle = Mathf.Atan2(nextProjectileDirection.y, nextProjectileDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            yield return new WaitForSeconds(SpawnCooldown * 0.5f);

            var projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity );
            projectile.DirProjectile = nextProjectileDirection;
            yield return new WaitForSeconds(SpawnCooldown * 0.5f);
        }
    }
}
