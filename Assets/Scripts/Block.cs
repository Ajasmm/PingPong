using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IDamagable
{
    [SerializeField] int life = 1;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] ParticleSystem particlesSystem;

    public Action OnDestroy;

    private void OnEnable()
    {
        ParticleSystem.MainModule main = particlesSystem.main;
        main.startColor = sprite.color;

        particlesSystem.gameObject.SetActive(false);
        particlesSystem.transform.parent = null;
    }

    public void Damage()
    {
        life--;
        if (life <= 0)
            this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if(OnDestroy != null) OnDestroy();
        OnDestroy = null;
        if (particlesSystem != null)
        {
            particlesSystem.gameObject.SetActive(true);
            particlesSystem.Play();
        }
    }
}
