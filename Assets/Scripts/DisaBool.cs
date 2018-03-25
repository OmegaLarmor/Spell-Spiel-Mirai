using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisaBool : MonoBehaviour
{

    public BooleanVariable boolean;
    private bool previousValue;

    public bool invert = false;

	public GameObject[] toDisable;

	bool toSet;

    // Use this for initialization
    void Start()
    {
        if (invert == false)
        {
            toSet = boolean.value;
        }

		else if (invert){
			
            toSet = !boolean.value;
		}

		

    }

    // Update is called once per frame
    void Update()
    {
        if (invert == false)
        {
            if (previousValue != boolean.value)
            {
				toSet = boolean.value;
            }
        }

        else if (invert)
        {
            if (previousValue == boolean.value)
            {
				toSet = !boolean.value;
            }
        }

		ChangeAllChildren(toSet);
		previousValue = toSet;
    }

	private void ChangeAllChildren(bool toSet){
		for (int i = 0; i < toDisable.Length; i++){
			toDisable[i].SetActive(toSet);
		}
	}
}
