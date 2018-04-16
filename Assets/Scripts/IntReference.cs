using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IntReference {

	public bool useConstant = false;
	public int constantValue;
	public int maxValue;
	public IntVariable variable;

	public int value {
		get { return useConstant ? constantValue : variable.value; }
	}

}
