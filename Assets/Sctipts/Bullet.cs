using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 100f;

    public float GetDamege()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
    
}
