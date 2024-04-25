using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootsProjectile
{
    Projectile Projectile { get; }
    Transform ProjectileSpawnPoint { get; }
    int ProjectileDamage { get; }
    float ProjectileSpeed { get; }
    float ProjectileTravelDistance { get; }
    void ShootProjectile();
}
