using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform[] nodes; //array of nodes (et in the Inspector)
    public float speed = 2f;
    private int currentNodeIndex = 0;
    public Spwaner spwaner;

    void Update()
    {
        if (nodes.Length == 0) return;

        //get the current nodes position
        Transform targetNode = nodes[currentNodeIndex];
        Vector3 targetPosition = targetNode.position;

        //move towards the target node
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //change the 
        if (Vector3.Distance(transform.position, targetPosition)< 0.1f)
        {
            currentNodeIndex++;
            if (currentNodeIndex >= nodes.Length)
            {
                currentNodeIndex = 0; // dont know if it needs this im leaving it here cuz i dont want to break it
                spwaner.EnemyDied();
                Destroy(gameObject);
            }
        }
    }

    //get the node refrences for the enemys
    public void SetNodes(Transform[] newNodes)
    {
        nodes = newNodes;
        currentNodeIndex = 0; // Reset to start at the first node
    }

    //get the spwaner refrence for the enemys
    public void SetSpwaner(Spwaner newSpwaner)
    {
        spwaner = newSpwaner;
    }

    //gizmo draw for path 
    private void OnDrawGizmos()
    {
        if (nodes == null || nodes.Length < 2) return;

        Gizmos.color = Color.red;
        for (int i = 0; i < nodes.Length - 1; i++)
        {
            if (nodes[i] != null && nodes[i + 1] != null)
            {
                Gizmos.DrawLine(nodes[i].position, nodes[i + 1].position);
            }
        }
    }
}
