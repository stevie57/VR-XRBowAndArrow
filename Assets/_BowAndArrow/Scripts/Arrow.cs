using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
    public float speed = 2000.0f;
    public Transform tip = null;

    private bool inAir = false;
    public Vector3 lastPosition = Vector3.zero;

    private Rigidbody rigidBody = null;

    private ParticleSystem particlesystem;
    public ArrowDamage arrowDamageSO;


    // damage for base arrow
    int arrowDamage = 0;



    protected override void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();
        particlesystem = GetComponentInChildren<ParticleSystem>();
        arrowDamage = arrowDamageSO.ArrowDamageAmount;
    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            CheckForCollision();
            lastPosition = tip.position;
        }
    }





    public virtual void CheckForCollision()
    {
        if (Physics.Linecast(lastPosition, tip.position))
            Stop();

    }

    //void CheckHitObject()
    //{
    //    RaycastHit hitInfo;

    //    if (Physics.Raycast(tip.position, transform.forward, out hitInfo, 1f))
    //    {
    //        Debug.Log("Arrow hit " + hitInfo.collider.gameObject.name);
    //        Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
    //        bool enemyhealth = enemy.health <= 0;

    //        enemy.TakeDamage(arrowDamage);
    //        if(enemyhealth)
    //        {
    //            Destroy(this);
    //        }
    //    }
    //}

    void CheckHitObject()
    {
        Collider[] colliders = Physics.OverlapSphere(tip.transform.position, 1f);

        foreach (Collider hit in colliders)
        {
            Debug.Log("Arrow hit " + hit.gameObject.name);
            Enemy enemy = hit.GetComponent<Enemy>();
            if(enemy != null)
            {
                
                enemy.TakeDamage(this, arrowDamage);
                transform.parent = enemy.transform; 
            }
        }


    }



    public virtual void Stop()
    {
        inAir = false;
        SetPhysics(false);
        particlesystem.Stop();
        CheckHitObject();

    }

    public void Release(float pullValue)
    {
        inAir = true;
        SetPhysics(true);

        MaskAndFire(pullValue);
        StartCoroutine(RotateWithVelocity());

        lastPosition = tip.position;


    }

    private void SetPhysics(bool usePhysics)
    {
        rigidBody.isKinematic = !usePhysics;
        rigidBody.useGravity = usePhysics;
    }

    private void MaskAndFire(float power)
    {
        //inherits from XRgrabInteractable which stores all colliders
        //there is only 1 collider so we can just use the first element of the array
        // trying to ensure our socket does not immedieatly pick it up
        colliders[0].enabled = false;
        // changing layer mask as additional prevention
        interactionLayerMask = 1 << LayerMask.NameToLayer("ignore");

        Vector3 force = transform.forward * (power * speed);
        rigidBody.AddForce(force);

        //activate particle system
        particlesystem.Play();
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();

        while (inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(rigidBody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    public new void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
    }

    public new void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);
    }

}
