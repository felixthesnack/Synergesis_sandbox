using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineQueue
{
    MonoBehaviour m_Owner = null;
    Coroutine m_InternalCoroutine = null;
    Queue<IEnumerator> actions = new Queue<IEnumerator>();
    public CoroutineQueue(MonoBehaviour aCoroutineOwner)
    {
        m_Owner = aCoroutineOwner;
    }
    public void StartLoop()
    {
        m_InternalCoroutine = m_Owner.StartCoroutine(Process());
    }
    public void StopLoop()
    {
        m_Owner.StopCoroutine(m_InternalCoroutine);
        m_InternalCoroutine = null;
    }
    public void PauseLoop()
    {
        m_Owner.StopCoroutine(m_InternalCoroutine);
    }
    public void ClearLoop()
    {
        m_InternalCoroutine = null;
    }
    public void EnqueueAction(IEnumerator aAction)
    {
        actions.Enqueue(aAction);
    }

    public void DequeueAction()
    {
        actions.Dequeue();
    }

    private IEnumerator Process()
    {
        while (true)
        {
            if (actions.Count > 0)
            {
                yield return m_Owner.StartCoroutine(actions.Dequeue());
                //Debug.Log("Queue count = " + actions.Count);
            }
            else
            {
                yield return null;
            }
        }
    }
    public void EnqueueWait(float aWaitTime)
    {
        actions.Enqueue(Wait(aWaitTime));
    }

    private IEnumerator Wait(float aWaitTime)
    {
        yield return new WaitForSeconds(aWaitTime);
    }

    public int GetCount()
    {
        return actions.Count;
    }
}
