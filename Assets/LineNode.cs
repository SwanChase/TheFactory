using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineNode : MonoBehaviour
{
    [SerializeField] private bool finishNode = false;
    [SerializeField] private bool OnHitCheatNode;
    public UnityEvent OnHitFinishNode;
    


    void Start()
    {
        if (OnHitFinishNode == null)
        {
            OnHitFinishNode = new UnityEvent();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("HitNode");
        }
        if (collision.tag == "Player" && finishNode)
        {
            Debug.Log("FinishNode");
            OnHitFinishNode.Invoke();
        }
        if (collision.tag == "Player" && OnHitCheatNode)
        {
            Debug.Log("CheaterNode");
            OnHitFinishNode.Invoke();
        }
    }
}
