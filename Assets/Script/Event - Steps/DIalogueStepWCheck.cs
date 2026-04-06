using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Steps/Dialogue")]
public class DialogueStepWCheck : EventStep
{
    // - Variables
    [TextArea]
    public List<string> lines;

    public void Awake()
    {

    }

    public override IEnumerator Execute(EventContext context)
    {
        //tryget uhh 
        if (!context.targets.TryGetValue("TicketMachine", out Transform sourceTransform))
        {
            Debug.LogWarning("!! -  no ticketmachine 'TicketMachine' - !!");
            yield break;
        }

        TicketMachine ticketMachine = sourceTransform.GetComponent<TicketMachine>();

        bool finished = false;

        DialogueUI.Instance.StartDialogue(lines, () => finished = true);

        yield return new WaitUntil(() => finished);
    }
}
