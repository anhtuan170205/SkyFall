using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;  // Âm thanh khi trúng đạn
    private AudioSource audioSource;
    private int health = 3;  // Máu của kẻ địch

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
