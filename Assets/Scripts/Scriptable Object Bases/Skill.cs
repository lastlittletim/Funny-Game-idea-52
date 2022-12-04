using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public virtual void OnUse(GameObject user, Vector2 shootDir) //called on the frame that the skill is used
    {

    }

    public virtual void OnUseStay(GameObject user, Vector2 shootDir) //called every frame the skill is active
    {

    }

    public virtual void OnUseEnd(GameObject user, Vector2 shootDir) //called on frame skill stops being used
    {

    }
}
