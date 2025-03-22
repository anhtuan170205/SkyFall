using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomb : MonoBehaviour
{
    public static event Action OnBombExploded;
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnBombExploded?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
