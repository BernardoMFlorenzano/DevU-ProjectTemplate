using Unity.Mathematics;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] ParticleSystem runParticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (math.abs(playerMovement.GetCurrentVelocity().x) > 0.1f && playerMovement.isGrounded)
        {
            if (!runParticles.isPlaying)
                runParticles.Play();
        }
        else if (runParticles.isPlaying)
        {
            runParticles.Stop();
        }
    }
}
