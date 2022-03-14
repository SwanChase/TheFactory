using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float range;
    bool closeEnough, needsCameraPos = false;
    [SerializeField]
    Transform player, camPos;

    [SerializeField]
    Material normal, highlighted;
    Renderer rend;

    private void Start()
    {
        if (player!)
        {
            Debug.LogWarning("something aint right fam check ya interactables");
        }
        if (needsCameraPos == true && camPos!)
        {
            Debug.LogWarning("something aint right fam check ya interactable cam pos");

        }

        rend = GetComponent<Renderer>();
        HighLighting(normal);
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
            Debug.Log("Hallo world");
            //Delegate of whatever is being interacted with like: to turn off/hijack movement controls for the Player
        }

    }

    public void HighLighting(Material mat)
    {
        rend.material = mat;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
