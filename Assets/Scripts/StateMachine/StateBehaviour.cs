using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBehaviour : ScriptableObject
{
    public abstract void Act(StateMachine brain);
}
