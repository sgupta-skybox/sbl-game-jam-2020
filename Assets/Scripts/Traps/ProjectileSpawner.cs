using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    Projectile ProjectilePrefab;

    [SerializeField]
    float SpawnCooldown = 1.0f;

    [SerializeField]
    AudioClip FireSound;

    AudioManager audioManager;
    [SerializeField]
    GameObject baseSprite;

    private void Start()
    {
        if (baseSprite)
            baseSprite.transform.localRotation = Quaternion.Inverse(transform.rotation);
    }

    void OnEnable()
	{
        audioManager = GameObject.FindGameObjectWithTag(AudioManager.NAME).GetComponent<AudioManager>();

        StartCoroutine(FireProjectile());
    }

    // Update is called once per frame
    IEnumerator FireProjectile()
    {
        while( true )
        {
            if (FireSound)
            {
                if(audioManager)
                    audioManager.PlayClip(FireSound);
            }
            Instantiate(ProjectilePrefab, transform.position + (transform.right * 3), transform.rotation);
            yield return new WaitForSeconds(SpawnCooldown * 1.0f);
        }
    }
}
