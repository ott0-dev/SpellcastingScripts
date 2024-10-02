using UnityEngine;
using System;

public class AnimatorTriggerer : MonoBehaviour
{
    public Animator animator; 

    public string trigger1Name = "Trigger1";
    public string trigger2Name = "Trigger2";
    public string trigger3Name = "Trigger3";

    public event Action Trigger1Event;
    public event Action Trigger2Event;
    public event Action Trigger3Event;
    
    public void Trigger1()
    {

        if (animator != null)
        {
            animator.SetTrigger(trigger1Name);
            Debug.Log("Trigger1 set");
        }

        Trigger1Event?.Invoke();
                  
    }

    public void Trigger2()
    {
        if (animator != null)
        {
            animator.SetTrigger(trigger2Name);
            Debug.Log("Trigger2 set");
        }

        Trigger2Event?.Invoke();
             
              
    }

    public void Trigger3()
    {
        if (animator != null)
        {
            animator.SetTrigger(trigger3Name);
            Debug.Log("Trigger3 set");
        }

        Trigger3Event?.Invoke();
                                     
    }
}
