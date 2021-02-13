using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveArrow : Arrow
{
    public float warmupTime = .5f;
    public int blinkFrequency = 1;
    public Material explosiveMaterial;
    public GameObject explosionParticle;
    Renderer bombRenderer;

    bool warm = false;

    //public override void CheckForCollision()
    //{
    //    base.CheckForCollision();
    //}

    public override void Stop()
    {
        base.Stop();
        WaitAndExplode();
    }

    void WaitAndExplode()
    {
        StartCoroutine(DetonateCoroutine());
    }

    IEnumerator DetonateCoroutine()
    {
        float elapsedTime = 0;
        float frameCount = 0;
        float lifeTime = 0;

        while (elapsedTime <= lifeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            frameCount++;

            var blink = Mathf.PingPong(frameCount * Time.timeScale, 10 / blinkFrequency);

            if (blink == 10 / blinkFrequency)
                ToggleMaterial();
            if(elapsedTime >= lifeTime - warmupTime && !warm)
                PrewarmExplosion();
            if (elapsedTime >= lifeTime)
                Detonate();
        }


    }

    void ToggleMaterial()
    {
        if (explosiveMaterial != null)
        {
            GetComponent<Renderer>().material = explosiveMaterial;
        }
    }

    void PrewarmExplosion()
    {
        warm = true;
        if(explosionParticle != null)
        {
            Debug.Log("prewarm explosion");
            var particle = Instantiate(explosionParticle, lastPosition, explosionParticle.transform.rotation);
            particle.SetActive(true);
            Destroy(particle, 3);
        }
    }

    void Detonate()
    {
        Debug.Log("Explode");
    }

    // Timer coroutine

    // change color based on time

    // warm up before explosion

    // explosion 



}
