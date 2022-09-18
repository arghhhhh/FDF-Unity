using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController_2 : MonoBehaviour
{
    private AudioPeer audioPeer;
    [Range(0,7)]
    public int band;
    public ParticleSystem m_System;
    public ParticleSystem.Particle[] m_Particles;
    void Start()
    {
        audioPeer = FindObjectOfType<AudioPeer>();
    }

    void LateUpdate()
    {
        InitializeIfNeeded();

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        float lerpedSize = Mathf.Lerp(0, 0.25f, audioPeer._audioBand[band]);
        if (numParticlesAlive > 0)
        {
            m_Particles[numParticlesAlive - 1].startSize = lerpedSize;
            m_Particles[numParticlesAlive - 1].startColor = Color.white;
        }

        m_System.SetParticles(m_Particles, numParticlesAlive);
    }

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = FindObjectOfType<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
    }
}
