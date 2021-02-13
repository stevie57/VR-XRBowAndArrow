using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    float smoothing = 1f;
    public Transform target;
    public bool isDead = false;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.transform;
        StartCoroutine(moveToTargetCoroutine(target));
        
    }

    IEnumerator moveToTargetCoroutine(Transform target)
    {
        while (Vector3.Distance(transform.position, target.position) > 0.05f && !isDead)
        {
            Vector3 lookAtPos = target.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(lookAtPos, transform.up);
            Quaternion targetRotation = new Quaternion(0, newRotation.y, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

            //check direction rotation is facing before moving
            float dot = Vector3.Dot(transform.forward, (target.position - transform.position).normalized);
            if(dot > 0.7)
            {
                float step = speed * Time.deltaTime;
                Vector3 targetPos = new Vector3(target.position.x, 0, target.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            }
            //Vector3 targetDirection = target.position - transform.position;
            //transform.Translate(targetDirection * speed * Time.deltaTime);

            //transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);

            yield return null;
        }

    }


}
