using UnityEngine;

// - main component of the stalkerbehaviour
public class StalkerBehaviour : MonoBehaviour
{
    private StalkerStateMachine _stalkerSoul; 

    private void OnEnable()
    {
        _stalkerSoul = new StalkerStateMachine();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}
