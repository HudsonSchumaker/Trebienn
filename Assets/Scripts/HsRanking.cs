using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/* 
	@author Hudson Schumaker
	@version 1.0.0
*/

public class HsRanking : MonoBehaviour {

	private string[] values;
	public Sprite loading;
	public GameObject [] ass50;

	void Start () {
		HsAdmob.instance.ShowBannerDown ();
		StartCoroutine(GetTop10 ());
	}

	void Update () {
	}

	IEnumerator GetTop10(){
		WWW www = new WWW("http://schumaker.com.br/Trebiann/gettop10.jsp");
		yield return www;
		string aux = www.text;
		values  = aux.Split( new char [] {';'});
		for(int k=0;k<10;k++){
			StartCoroutine (GetImageAss (k,values[k+1].Trim()));
		}
		www.Dispose ();
		//HsAdmob.instance.ShowBannerDown ();
	}

	IEnumerator GetImageAss(int n,string ass){
		Image img = ass50[n].GetComponent<Image>();
		img.sprite = loading;

		WWW www = new WWW("http://schumaker.com.br/App/photos/"+ass+".jpg");
		yield return www;

		Rect rec = new Rect(0, 0, www.texture.width, www.texture.height);
		Sprite spriteToUse = Sprite.Create(www.texture,rec,new Vector2(0.5f,0.5f),100);
		www.Dispose ();
		Image buttonImage = ass50[n].GetComponent<Image>();
		buttonImage.sprite = spriteToUse;

	}

	public void Back(){
		HsAdmob.instance.RemoveBanners ();
		SceneManager.LoadScene ("_MainMenu");
	}
}
