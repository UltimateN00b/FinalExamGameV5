using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour {

	public static GameObject SearchChild(string child, GameObject searchObject)
	{
		GameObject returnChild = null;

		for (int i = 0; i < searchObject.transform.childCount; i++) {
			GameObject currChild = searchObject.transform.GetChild (i).gameObject;
			if (currChild.name.Equals (child)) {
				returnChild = currChild;
			}
		}
			
		return returnChild;
	}

	public static void DestroyWithParticles (GameObject obj, GameObject particles, float delay)
	{
		Destroy (obj);
		GameObject newParticles = Instantiate (particles, obj.transform.position, Quaternion.identity);
		Destroy (newParticles, delay);
	}

	public static void MakeParticles (GameObject obj, GameObject particles, float delay)
	{
		GameObject newParticles = Instantiate (particles, obj.transform.position, Quaternion.identity);
		Destroy (newParticles, delay);
	}

    public static int GetActiveChildren(GameObject obj)
    {
        int numChildren = 0;

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            if (obj.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                numChildren++;
            }
        }

        return numChildren;
    }
}
