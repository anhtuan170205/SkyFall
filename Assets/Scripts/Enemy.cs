using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    void Update()
    {
        Climb();
    }
    private void Climb()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            gameObject.SetActive(false);
        }
    }
}
