using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaAdjust : MonoBehaviour {

	public Slider alphaSlider;
	public GameObject depthImage;

	// Use this for initialization
	void Start () {
		depthImage.GetComponent<Renderer>().material.color = new Color(1, 1, 1, alphaSlider.value);
		alphaSlider.onValueChanged.AddListener((value)=>{
			depthImage.GetComponent<Renderer>().material.color = new Color(1, 1, 1, value);
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}