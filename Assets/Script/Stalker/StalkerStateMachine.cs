using UnityEngine;
// - compendium of states
public class StalkerStateMachine 
{
    public StalkerMainState stalkerRoamingState()
    {
        return new StalkerRoamingState();
    }
}
