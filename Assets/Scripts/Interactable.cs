using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float r;
    bool closeEnough;
    [SerializeField]
    Transform player;

    [SerializeField]
    Material normal, highlighted;
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        HighLighting(normal);
    }

    void Update()
    {
        
        if (Vector3.Distance(player.position, transform.position) <= r)
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

        }
        
    }

    public void HighLighting(Material mat)
    {
        rend.material = mat;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, r);
    }
}
