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
		InvokeRepeating ("ManageAds",0.1f,30.5f);
		StartCoroutine(GetTop10 ());
	}
		
	IEnumerator GetTop10(){
		WWW www = new WWW("http://schumaker.com.br/Trebiann/gettop10.jsp");
		yield return www;
		string aux = www.text;
		values  = aux.Split( new char [] {';'});
		int p = 10;//position
		for(int k=0;k<10;k++){
			StartCoroutine (GetImageAss (p,k,values[k+1].Trim()));
			p--;
		}
		www.Dispose ();
	}

	IEnumerator GetImageAss(int p,int n,string ass){
		Image img = ass50[n].GetComponent<Image>();
		img.sprite = loading;
		ass50[n].GetComponentInChildren<Text>().text = p + "º";

		WWW www = new WWW("http://schumaker.com.br/App/photos/"+ass+".jpg");
		yield return www;

		Rect rec = new Rect(0, 0, www.texture.width, www.texture.height);
		Sprite spriteToUse = Sprite.Create(www.texture,rec,new Vector2(0.5f,0.5f),100);
		www.Dispose ();
		Image buttonImage = ass50[n].GetComponent<Image>();
		buttonImage.sprite = spriteToUse;
	}

	private void ManageAds(){
		HsAdmob.instance.RemoveBanners ();
		new	WaitForSeconds (5);
		HsAdmob.instance.ShowBannerDown ();
	}

	public void Back(){
		HsAdmob.instance.RemoveBanners ();
		SceneManager.LoadScene ("_MainMenu");
	}

	public void Reload(){
		HsAdmob.instance.RemoveBanners ();
		SceneManager.LoadScene ("_Ranking");
	}

}