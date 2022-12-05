using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Projectile Skill", menuName = "Skill/Projectile Skill")]
public class ProjectileSkill : Skill
{
    public List<ProjectileGroup> onUseProjectiles; //called on the frame that the skill is used
    public List<ProjectileGroup> onUseStayProjectiles; //called every frame the skill is active
    public List<ProjectileGroup> onUseEndProjectiles; //called on frame skill stops being used

    public override void OnUse(GameObject user, Vector2 shootDir)
    {
        base.OnUse(user, shootDir); //call original
        foreach (ProjectileGroup projectileGroup in onUseProjectiles) //loop through all projectileGroup s
        {
            Debug.Log("calling shoot");
            Shoot(user, shootDir, projectileGroup);
        }
    }

    public override void OnUseStay(GameObject user, Vector2 shootDir)
    {
        base.OnUseStay(user, shootDir);

        float[] timers = new float[onUseStayProjectiles.Count];
        for (int i = 0; i < onUseStayProjectiles.Count; ++i)
        {
            if (timers[i] >= onUseStayProjectiles[i].shootInterval) //if timer above interval
            {
                Shoot(user, shootDir, onUseStayProjectiles[i]); //shoot
                timers[i] = 0f;
            }
        }
    }

    public override void OnUseEnd(GameObject user, Vector2 shootDir)
    {
        base.OnUse(user, shootDir); //call original
        foreach (ProjectileGroup projectileGroup in onUseEndProjectiles) //loop through all projectileGroup s
        {
            Shoot(user, shootDir, projectileGroup);
        }
    }

    public void Shoot(GameObject user, Vector2 shootDir, ProjectileGroup projectileGroup)
    {
        if(projectileGroup.particlePre != null) Instantiate(projectileGroup.particlePre, user.transform.position, Quaternion.identity); //create particle if particlePre is assigned
        for (int i = 0; i < projectileGroup.projectileCount; ++i)
        {
            GameObject projectileObj = Instantiate(projectileGroup.projectilePre, user.transform.position, Quaternion.identity); //create projectile, save to projectileObj
            if (projectileGroup.projectileSpread > 0)
            {
                float shootAngle = Vector2.SignedAngle(Vector2.right, shootDir);
                shootAngle += UnityEngine.Random.Range(-projectileGroup.projectileSpread, projectileGroup.projectileSpread); //apply random spread
                shootDir = new Vector2(Mathf.Cos(shootAngle / 180 * Mathf.PI), Mathf.Sin(shootAngle / 180 * Mathf.PI));
            }
            projectileObj.GetComponent<Rigidbody2D>().velocity = shootDir * UnityEngine.Random.Range(projectileGroup.projectileSpeedMin, projectileGroup.projectileSpeedMax);
        }
    }

    [Serializable]
    public struct ProjectileGroup
    {
        public GameObject projectilePre;
        public GameObject particlePre; //whatever's here will NOT be shot, and will only be created once per time the ProjectileGroup is called

        public int projectileCount; //the amount of projectiles created
        public float projectileSpread; //shotgun spread angle
        public float projectileSpeedMin;
        public float projectileSpeedMax;
        public float shootInterval; //used only for onStay, determines the time between each shot
    }
}
