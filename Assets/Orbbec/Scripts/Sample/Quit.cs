﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrbbecUnity
{
public class Quit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
}