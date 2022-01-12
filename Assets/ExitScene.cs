using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour {

	private bool isLoading = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButtonUp(1))
		{
			if(!isLoading)
			{
				SceneManager.LoadScene(0);
				isLoading = true;
			}
		}
	}
}
