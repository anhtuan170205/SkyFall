using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomb : MonoBehaviour
{
    public static event Action OnBombExploded;
    [SerializeField] private Transform explosionVfxPrefab;
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnBombExploded?.Invoke();
            Instantiate(explosionVfxPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            AudioManager.Instance.PlayExplosionClip();
        }
    }
}
