using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IDamagable
{
    [SerializeField] int life = 1;

    public Action OnDestroy;
   
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
    }
}
