using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public List<Transformation> transformations;

    public Interaction next;

    public bool unlocked;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure transformations are sorted by priority
        transformations = transformations.OrderBy(t => t.priority).ToList();

        // Set finished to false
        transformations.ForEach(t => t.finished = false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Interact()
    {
        if(unlocked)
        {
            int maxPriority = transformations[transformations.Count - 1].priority;

            // Iterate over items by priority
            for(int currentPriority = 1; currentPriority <= maxPriority; currentPriority++)
            {
                // Get all transformations that have current priority
                List<Transformation> tformations = transformations.Where(t => t.priority == currentPriority).ToList();\
                
                // Apply each transformation
                tformations.ForEach(t => t.Apply());
                
                // Wait until each transformation in this priority is finished
                while(tformations.Where(t => !t.finished) != null)
                {
                    yield return null;
                }
            }

            
        }

        yield return null;
    }

    void ApplyTransformations(int order)
    {

    }
}
