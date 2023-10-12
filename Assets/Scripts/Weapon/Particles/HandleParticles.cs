using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleParticles : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Play();
    }

    void DestroyParticles()
    {
        GameObject.Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(DestroyParticles), 1);
    }
}
