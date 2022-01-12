using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour {

	public string sceneName;

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(()=>{
			SceneManager.LoadScene(sceneName);
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
