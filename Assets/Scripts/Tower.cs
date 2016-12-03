using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(TurretData))]
public class Tower : MonoBehaviour
{
    Transform muzzle;
    TurretData turretData;

    public GameObject bulletPrefab;
    public int cost = 5;

    public float range = 10f;
    public float damage = 1;
    public float radius = 0;

    float fireCooldown = 0.5f;
    float fireCooldownLeft = 0;

	// Use this for initialization
	void Start ()
    {
        // Gets the Component with the info about the Turret
        turretData = GetComponent<TurretData>();

        // TODO: change the Muzzle on upgrade. Maybe store the muzzle position on Turret Data
        // Gets the Muzzle of the tower to use in the ShootAt Method
        muzzle = turretData.CurrentLevel.visualization.transform.FindChild("Muzzle");
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        // TODO: Optimize this!

        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        Enemy nearestEnemy = null;
        float dist = Mathf.Infinity;

        foreach(Enemy e in enemies)
        {
            float d = Vector3.Distance(this.transform.position, e.transform.position);

            if(nearestEnemy == null || d < dist)
            {
                nearestEnemy = e;
                dist = d;
            }
        }

        if (nearestEnemy == null)
        {
            //Debug.Log("No Enemies");
            return;
        }
        
        Vector3 dir = nearestEnemy.transform.position - this.transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        
        // TODO: Not Working Properly wrong rotation. Offset the Z 90 degrees somehow
        turretData.CurrentLevel.visualization.transform.rotation = Quaternion.Euler(turretData.CurrentLevel.visualization.transform.rotation.eulerAngles.x, lookRot.eulerAngles.y, (turretData.CurrentLevel.visualization.transform.rotation.eulerAngles.z - 90));
        //turretTransform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0); // OLD

        Debug.DrawLine(muzzle.position, nearestEnemy.transform.position, Color.green);

        fireCooldownLeft -= Time.deltaTime;

        if(fireCooldownLeft <= 0 && dir.magnitude <= range)
        {
            fireCooldownLeft = fireCooldown;
            ShootAt(nearestEnemy);
        }
    }
    
    void ShootAt(Enemy e)
    {
        // TODO: Fire out the tip!
        // OLD: GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);

        Bullet b = bulletGO.GetComponent<Bullet>();
        b.target = e.transform;
        b.damage = damage;
        b.radius = radius;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1 + range);
    }

    // TODO: TOWER UPGRADES

    // Manager for tower upgrade. When player clicks on the tower, the tower tells the manager that it is active, the manager show the stats of the tower
    // Add a collider to the tower so it can use the OnMouseDown/OnMouseUp

    
    private bool canUpgradeTurret()
    {
        
        TurretData.TurretLevel nextLevel = turretData.getNextLevel();

        if(nextLevel != null)
        {
            return true;
        }

        return false;
    }

    void OnMouseUp()
    {
        if(canUpgradeTurret())
        {
            //turretData = GetComponent<TurretData>();
            turretData.increaseLevel();
        }
    }
}
