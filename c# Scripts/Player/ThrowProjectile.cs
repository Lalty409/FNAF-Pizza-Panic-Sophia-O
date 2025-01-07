using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;

    private void Awake()
    {
        HoldingObject holdingObject = GameObject.FindGameObjectWithTag("Player").GetComponent<HoldingObject>();
        holdingObject.OnShoot += HoldingObject_OnShoot; //subscribes to event call

    }

    private void HoldingObject_OnShoot(object sender, HoldingObject.OnShootEventArgs e)
    {
        HoldingObject holdingObject = GameObject.FindGameObjectWithTag("Player").GetComponent<HoldingObject>();
        Transform projectileTransform = Instantiate(projectilePrefab, e.holdingPosition, Quaternion.identity).transform;
        Projectile projectile = projectileTransform.GetComponent<Projectile>();
        projectile.MakeProjectile(e.ProjectileDirection, e.projectileSpriteVal);
        holdingObject.holdingVal = 0;
    }
}