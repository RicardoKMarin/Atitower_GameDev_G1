using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    //[SerializeField] private AudioClip shotSound;
    //[SerializeField] private AudioSource audioSourceShot;

    private void Start()
    {
        
        /*if (shotSound != null && audioSourceShot != null)
        {
            audioSourceShot.clip = shotSound;
            audioSourceShot.Play();
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        //Debug.Log("tirotitotro");

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }
}