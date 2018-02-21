//Borrowed from Ryan Hipple, Unite Austin 2017

using UnityEngine;
using UnityEngine.UI;

public class ImageScaleSetter : MonoBehaviour {

	public FloatReference variable;
	public FloatReference max;

	public Image image;

	void Update(){
		//image.fillAmount = Mathf.Clamp01(variable.value / max.value);
		image.rectTransform.localScale = new Vector3(1,2,1);
	}
}
