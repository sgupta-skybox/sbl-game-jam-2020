using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableBase : MonoBehaviour
{
    public bool IsTriggered
    {
        get { return _IsTriggered; } 
        set
        {
            if (value != _IsTriggered)
            {
                _IsTriggered = value;
                if (value)
                {
                    OnTriggered();
                }
                else
                {
                    OnUntriggered();
                }
            }
        }
    }

    protected virtual void OnTriggered() { }
    protected virtual void OnUntriggered() { }

    private bool _IsTriggered = false;
}
