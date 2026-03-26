using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveHeldObject()
    {
        Vector3 targetPos = holdPoint.position;
        Vector3 currentPos = holdObject.position;

        Vector3 newPos = Vector3.Lerp(currentPos, targetPos, holdSmoothing * Time.fix)

    }

    
}
