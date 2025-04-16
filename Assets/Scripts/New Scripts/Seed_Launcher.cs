using System;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class Seed_Launcher : ProjectileWeapon
{
    protected InputManager _inputManager;
    public Seed_Holder seed_Holder;
    public Projectile_Mixer projectile_Mixer;

    [Tooltip("the object pooler used to spawn projectiles, if left empty, this component will try to find one on its game object")]
    public MMMultipleObjectPooler MMObjectPooler;

    public override void Initialization()
    {
        base.Initialization();
        _inputManager = Owner.LinkedInputManager;
        seed_Holder = Owner.GetComponent<Seed_Holder>();
        projectile_Mixer = Owner.GetComponent<Projectile_Mixer>();
    }

    public override void WeaponUse()
    {
        if (seed_Holder.IsEmpty) return;
        ElementalProjectile _seed = null;


        if (_inputManager.SecondaryShootButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed)
        {
            _seed = projectile_Mixer.GetProjectilePref(seed_Holder.GiveSeed(1));
        }
        else
            _seed = projectile_Mixer.GetProjectilePref(seed_Holder.GiveSeed(0));


        DetermineSpawnPosition();

        for (int i = 0; i < ProjectilesPerShot; i++)
        {
            SpawnSeed(SpawnPosition, _seed, true);
            PlaySpawnFeedbacks();
        }
    }

    public ElementalProjectile SpawnSeed(Vector3 spawnPosition, ElementalProjectile projSo , bool triggerObjectActivation = true)
    {
        /// we get the next object in the pool and make sure it's not null
        //Seed NextSeed = MMObjectPooler.GetPooledGameObjectOfType(seed.Name).GetComponent<Seed>();
        ElementalProjectile NextProj = Instantiate(projSo);
        if (OverrideSpeed > 0)
            NextProj.Speed = OverrideSpeed;
        if (DoesOverrideLayerMask)
            NextProj.GetComponent<DamageOnTouch>().TargetLayerMask = OverridingMask;

        // mandatory checks
        if (NextProj == null) { return null; }

        // we position the object
        NextProj.transform.position = spawnPosition;
        if (_projectileSpawnTransform != null)
        {
            NextProj.transform.position = _projectileSpawnTransform.position;
        }
        // we set its direction

        Projectile projectile = NextProj.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.SetWeapon(this);
            if (Owner != null)
            {
                projectile.SetOwner(Owner.gameObject);
            }
        }

        if (projectile != null)
        {
            if (RandomSpread)
            {
                _randomSpreadDirection.x = UnityEngine.Random.Range(-Spread.x, Spread.x);
                _randomSpreadDirection.y = UnityEngine.Random.Range(-Spread.y, Spread.y);
                _randomSpreadDirection.z = UnityEngine.Random.Range(-Spread.z, Spread.z);
            }
            

            Quaternion spread = Quaternion.Euler(_randomSpreadDirection);

            if (Owner == null)
            {
                projectile.SetDirection(spread * transform.rotation * DefaultProjectileDirection, transform.rotation, true);
            }
            else
            {
                if (Owner.CharacterDimension == Character.CharacterDimensions.Type3D) // if we're in 3D
                {
                    projectile.SetDirection(spread * transform.forward, transform.rotation, true);
                }
                else // if we're in 2D
                {
                    Vector3 newDirection = (spread * transform.right) * (Flipped ? -1 : 1);
                    if (Owner.Orientation2D != null)
                    {
                        projectile.SetDirection(newDirection, spread * transform.rotation, Owner.Orientation2D.IsFacingRight);
                    }
                    else
                    {
                        projectile.SetDirection(newDirection, spread * transform.rotation, true);
                    }
                }
            }

            if (RotateWeaponOnSpread)
            {
                this.transform.rotation = this.transform.rotation * spread;
            }
        }

        if (triggerObjectActivation)
        {
            if (NextProj.GetComponent<MMPoolableObject>() != null)
            {
                NextProj.GetComponent<MMPoolableObject>().TriggerOnSpawnComplete();
            }
        }
        return (NextProj);
    }


}
