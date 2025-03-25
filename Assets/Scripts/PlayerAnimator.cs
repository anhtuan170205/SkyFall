using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Start()
    {
        BombManager.Instance.OnBombSpawned += BombManager_OnBombSpawned;
    }

    private void BombManager_OnBombSpawned()
    {
        animator.SetTrigger("isShoot");
    }
}
