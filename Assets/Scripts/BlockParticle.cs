using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockParticle : MonoBehaviour
{
    [SerializeField] AudioSource source;

    private void OnEnable()
    {
        source.Play();
    }

    private void OnDisable()
    {
        source.Stop();
    }
}
