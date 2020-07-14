using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killable : MonoBehaviour
{
    public float health = 300f;
    public string bulletTag;
    public ParticleSystem particleSys;
    public AudioClip deathSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet && bullet.gameObject.CompareTag(bulletTag))
        {
            health -= bullet.GetDamege();
            Debug.Log(this + " was hit by " + bullet);
            bullet.Hit();
            if (health <= 0)
            {
                if (gameObject.CompareTag("Hostile"))
                {
                    ScoreKeeper.Score(150);
                }
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                Instantiate(particleSys, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
