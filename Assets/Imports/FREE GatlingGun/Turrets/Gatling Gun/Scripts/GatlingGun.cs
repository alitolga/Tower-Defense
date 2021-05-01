using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingGun : MonoBehaviour
{
    // target the gun will aim at
    //Transform go_target;

    // Gameobjects need to control rotation and aiming
    //public Transform go_baseRotation;
    //public Transform partToRotate;
    //public Transform go_barrel;

    // Gun barrel rotation
    //public float barrelRotationSpeed;
    //float currentRotationSpeed;

    // Distance the turret can aim and fire from
    //public float firingRange;

 

    // Used to start and stop the turret firing
    bool canFire = false;


    public Transform target;
    
    [Header("Attributes")]
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public float firingRange;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float rotationSpeed = 10f;

    // Particle system for the muzzel flash
    public ParticleSystem muzzelFlash;

    void Start()
    {
        // Set the firing range distance
        this.GetComponent<SphereCollider>().radius = firingRange;

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= firingRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

    void Update()
    {
        if (target == null)
            return;
        //AimAndFire();

        Debug.Log("Rotating");
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = lookRotation.eulerAngles;
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        //partToRotate.transform.LookAt(target, Vector3.up);

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        //Instantiate<Transform>(muzzelFlash.transform, partToRotate.position, partToRotate.rotation);
        muzzelFlash.Play(true);
    }



    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position to show the firing range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, firingRange);
    }

    // Detect an Enemy, aim and fire
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("Enemy detected");
    //        go_target = other.transform;
    //        canFire = true;
    //    }

    //}
    //// Stop firing
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        canFire = false;
    //    }
    //}

    void AimAndFire()
    {
        // Gun barrel rotation
        //if (go_barrel != null)
            //go_barrel.transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);

        // if can fire turret activates
        if (canFire)
        {
            // start rotation
            Debug.Log("Rotation started");
            //currentRotationSpeed = barrelRotationSpeed;

            // aim at enemy
            //Vector3 baseTargetPostition = new Vector3(go_target.position.x, this.transform.position.y, go_target.position.z);
            //Vector3 gunBodyTargetPostition = new Vector3(go_target.position.x, go_target.position.y, go_target.position.z);

            Debug.Log("Looking at enemy");
            //if(go_baseRotation != null)
                //go_baseRotation.transform.LookAt(baseTargetPostition);
            //partToRotate.transform.LookAt(gunBodyTargetPostition);

            // start particle system 
            if (!muzzelFlash.isPlaying)
            {
                Debug.Log("Attacking");
                muzzelFlash.Play();
            }
        }
        else
        {
            // slow down barrel rotation and stop
            //currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, 10 * Time.deltaTime);

            // stop the particle system
            if (muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop();
            }
        }
    }
}