using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// - Step for handling the Dialogue Step in the event system
// - Daniel Bruijn

[CreateAssetMenu(menuName = "Events/Steps/Dialogue")]
public class DialogueStep : EventStep
{
    // - Variables
    [TextArea]
    public List<string> lines;

    public override IEnumerator Execute(EventContext context)
    {
        bool finished = false;

        DialogueUI.Instance.StartDialogue(lines, () => finished = true);

        yield return new WaitUntil(() => finished);
    }
}