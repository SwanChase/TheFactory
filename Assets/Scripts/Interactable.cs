using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    public float range;
    bool closeEnough, needsCameraPos = false;
    [SerializeField]
    Transform player, camPos;

    [SerializeField]
    Material normal, highlighted;

    Renderer rend;
    [SerializeField]
    Renderer TargetModel;

    [SerializeField]
    public UnityEvent onInteraction;

    private void Start()
    {
        if (player!)
        {
            Debug.LogWarning("something aint right fam check ya Player");
        }
        if (TargetModel!)
        {
            Debug.LogWarning("something aint right fam check ya Targetmodel");
        }
        if (needsCameraPos == true && camPos!)
        {
            Debug.LogWarning("something aint right fam check ya cam pos");
        }

        Debug.Log(normal);
        //TargetModel.material=normal;
        HighLighting(normal);

        if (onInteraction == null)
        {
            onInteraction = new UnityEvent();
        }
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= range)
        {
            closeEnough = true;
            HighLighting(highlighted);
        }
        else
        {
            closeEnough = false;
            HighLighting(normal);
        }

        if (closeEnough && Input.GetKeyUp(KeyCode.E))
        {
            HighLighting(normal);
            onInteraction.Invoke();
        }
    }

    public void HighLighting(Material mat)
    {
        TargetModel.material = mat;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void OutfitChange()
    {

    }
}
