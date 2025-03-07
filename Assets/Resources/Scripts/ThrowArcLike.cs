﻿using UnityEngine;
using System.Collections;

public class ThrowArcLike : MonoBehaviour
{
    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;
    private Transform myTransform;

    public bool shouldRotate = false;

    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {

    }

    void Update()
    {
        if (shouldRotate)
        {
            Projectile.Rotate(new Vector3(0, 0, 0.8f));
        }
    }



    public IEnumerator SimulateProjectile()
    {
        while ( Projectile != null)// &&|| Projectile != null
        {
            if(transform == null)
                break;
            // Short delay added before Projectile is thrown
            yield return new WaitForSecondsRealtime(0.5f);

            // Move projectile to the position of throwing object + add some offset if needed.
            Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

            // Calculate distance to target
            float target_Distance = Vector3.Distance(Projectile.position, Target.position);

            // Calculate the velocity needed to throw the object to the target at specified angle.
            float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

            // Extract the X  Y componenent of the velocity
            float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
            float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

            // Calculate flight time.
            float flightDuration = target_Distance / Vx;

            // Rotate projectile to face the target.
            Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

            float elapse_time = 0;

            while (elapse_time < flightDuration && Projectile != null)
            {
                Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
                //Projectile.Rotate()

                elapse_time += Time.deltaTime;

                yield return null;
            }
            // return to initital position
            if (Projectile != null)
                Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

            /* if (elapse_time >= 2 * flightDuration)
            {
                this.enabled = false;
            } */
            // if (gameObject == null && !gameObject.activeInHierarchy)
            // {
            //     StopAllCoroutines();
            // }
            // if (Projectile.gameObject == null && !Projectile.gameObject.activeInHierarchy)
            // {
            //     StopAllCoroutines();
            // }
        }
        //this.enabled = false;
        //gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Debug.Log("Getting destroyed :/");
        StopCoroutine(SimulateProjectile());
        this.enabled = false;
        gameObject.SetActive(false);

    }
}