using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
	@author Hudson Schumaker
	@version 1.0.0
*/

public class HsMainMenu : MonoBehaviour {

	private void Awake(){
	}

	private void Start () {
		HsAdmob.instance.ShowBannerDown ();
	}

	public void PlayGame(){
		SceneManager.LoadScene ("_Game");
	}

	public void Ranking(){
		SceneManager.LoadScene ("_Ranking");
	}

	public void Exit(){
		Application.Quit();
	}
}
