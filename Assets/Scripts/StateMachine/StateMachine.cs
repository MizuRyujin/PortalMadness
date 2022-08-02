using UnityEngine;

public class StateMachine : MonoBehaviour
{	
    // Have default state as constant
    // Have a collection of possible states
    
    [SerializeField] private State[] _states;


	/// <summary>>
    /// Update is called once per frame
	/// </summary>>
    private void Update()
    {
        for (int i = 0; i < _states.Length; i++)
        {
            _states[i].OnUpdate(this);
        }
    }
}
