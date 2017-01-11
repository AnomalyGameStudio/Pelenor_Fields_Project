using UnityEngine;
using System.Collections;

public class EnemyAttackAI : MonoBehaviour
{
    // PUBLIC VARIABLES
    public GameObject bulletPrefab;

    public float range = 20f;
    public float fireRate = 1f;
    public float damage = 1f;
    public float radius = 0f;

    // PRIVATE VARIABLES
    Transform muzzle;

    float timeToFire = 0;

    void Start ()
    {
        muzzle = transform.FindChild("Muzzle");
    }
	
	// Update is called once per frame
	void Update ()
    {
        // TODO: Optimize this!

        GameObject cargoShip = GameObject.FindGameObjectWithTag("Player");

        float dist = Vector3.Distance(muzzle.transform.position, cargoShip.transform.position);

        if(dist < range && Time.time > timeToFire)
        {
            // FIRE
            ShootAt(cargoShip);
        }
    }

    void ShootAt(GameObject target)
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);

        Bullet b = bulletGO.GetComponent<Bullet>();
        b.target = target.transform;
        b.damage = damage;
        b.radius = radius;

        timeToFire = Time.time + 1 / fireRate;
    }
}
