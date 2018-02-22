//Borrowed from Ryan Hipple, Unite Austin 2017

using UnityEngine;
using UnityEngine.UI;

public class ImageScaleSetter : MonoBehaviour {

	public FloatReference variable;
	public FloatReference max;

	public bool scaleX;
	public bool scaleY;
	public bool scaleZ;

	public Image image;

	void Update(){
		float scaleAmount = Mathf.Clamp01(variable.value / max.value);
		float x = (scaleX ? scaleAmount : 1);
		float y = (scaleY ? scaleAmount : 1);
		float z = (scaleZ ? scaleAmount : 1);
		image.rectTransform.localScale = new Vector3(x,y,z);
	}
}
