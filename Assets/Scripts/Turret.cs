using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
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

 
    public enum FiringMode { PROJECTILE, MACHINEGUN, LASER};


    public Transform target;

    public Transform firePosition;
    
    public FiringMode firingMode;

    // Particle system for the muzzel flash (machine gun)
    public ParticleSystem muzzelFlash;
    
    // Turrets that shoot regular bullets or missiles
    public GameObject projectilePrefab;

    // Turrets that shoot lasers
    public Transform laserEffect;

    [Header("Attributes")]
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public float firingRange;
    public float damageAmount; // For machineguns and lasers

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float rotationSpeed = 10f;

    

    //public TurretRotation turretRotation;

    void Start()
    {
        // Set the firing range distance
        //this.GetComponent<SphereCollider>().radius = firingRange;

        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        //turretRotation = GetComponent<TurretRotation>();
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
        {
            if(muzzelFlash != null && muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop(true);
            }
            return;
        }

        LookAtTarget();

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void LookAtTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Ya da transform.LookAt();
    }

    void Shoot()
    {
        //Instantiate<Transform>(muzzelFlash.transform, firePosition.position, firePosition.rotation);
        if(firingMode == FiringMode.MACHINEGUN)
        {
            if(!muzzelFlash.isPlaying)
                muzzelFlash.Play(true);
            DamageEnemy(target);
        }
        if(firingMode == FiringMode.PROJECTILE)
        {
            GameObject bulletObject = Instantiate<GameObject>(projectilePrefab, firePosition.position, firePosition.rotation);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            if (bullet != null)
                bullet.Seek(target);
        }
    }

    void DamageEnemy(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damageAmount);
            Debug.Log("Health: " + e.health);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position to show the firing range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, firingRange);
    }

}