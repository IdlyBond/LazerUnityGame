using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehaviour : Killable
{
    public GameObject enemyBulletPrefab;
    public float bulletSpeed = 15f;
    public float shotsPerSec = 8f;
    private GameObject player;
    public AudioClip fireSound;

    

    private void Start()
    {
        player = GameObject.Find("Player");

    }

    private void Update()
    {
        
        if (Random.value * Time.deltaTime * shotsPerSec < 0.005f && player)
        {
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);
        }
    }
}
