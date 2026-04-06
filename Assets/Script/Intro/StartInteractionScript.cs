using UnityEngine;

public class StartInteractionScript : MonoBehaviour
{
    [SerializeField] private AudioSource _introDialogue;
    [SerializeField] private GameObject[] _enable;
    private bool _interacted;


    private void OnTriggerEnter(Collider other)
    {
        if (!_interacted)
        {
            _introDialogue.Play();
            for (int i = 0; i < _enable.Length; i++)
            {
                _enable[i].SetActive(true);
            }
        }
        _interacted = true;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _enable.Length; i++)
        {
            _enable[i].SetActive(false);
        }
    }
}
