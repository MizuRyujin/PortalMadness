using UnityEngine;


[CreateAssetMenu(fileName = "State", menuName = "State Machine/State")]
public class State : ScriptableObject
{
    [SerializeField] private StateBehaviour[] _behaviours;


    public void OnUpdate(StateMachine brain)
    {
        DoActions(brain);
    }

    private void DoActions(StateMachine brain)
    {
        for (int i = 0; i < _behaviours.Length; i++)
        {
            _behaviours[i].Act(brain);
        }
    }
}
