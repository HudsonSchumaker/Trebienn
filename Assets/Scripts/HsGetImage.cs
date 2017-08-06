using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/* 
	@author Hudson Schumaker
	@version 1.0.0
*/
public class HsGetImage : MonoBehaviour {

	public GameObject assLeft;
	public GameObject assRight;
	public Sprite loading;

	private string assLeftName;
	private string assRightName;
	private int clicks;


	void Start(){
	 	HsAdmob.instance.ShowBannerDown ();
		StartCoroutine(GetFristTwo());
		clicks = 0;
	}
		
	void FixedUpdate(){
		if(clicks>=20){
			clicks = 0;
			HsAdmob.instance.RemoveBanners ();
			HsAdmob.instance.ShowVideo ();
			HsAdmob.instance.LoadBigBanner ();
			HsAdmob.instance.ShowBannerDown ();
		}
	} 
		
	public void SetScoreLeft(){
		StartCoroutine (SetScoreLoser (assRightName));
		StartCoroutine (SetScoreWinner(assLeftName));
		StartCoroutine (GetOneRight ());
		clicks++;
	}

	public void SetScoreRight(){
		StartCoroutine (SetScoreLoser (assLeftName));
		StartCoroutine (SetScoreWinner(assRightName));
		StartCoroutine (GetOneLeft ());
		clicks++;
	}

	private IEnumerator GetOneLeft(){
		WWW www = new WWW("http://schumaker.com.br/Trebiann/getone.jsp?w="+assRightName+"&l="+assLeftName+"");
		yield return www;
		string aux = www.text;
		www.Dispose ();
		assLeftName = aux.Trim ();
		StartCoroutine (SetAssLeft(assLeftName));
	}

	private IEnumerator GetOneRight(){
		WWW www = new WWW("http://schumaker.com.br/Trebiann/getone.jsp?w="+assRightName+"&l="+assLeftName+"");
		yield return www;
		string aux = www.text;
		www.Dispose ();
		assRightName = aux.Trim ();
		StartCoroutine (SetAssRight(assRightName));
	}

	private IEnumerator GetFristTwo(){
		WWW www = new WWW("http://schumaker.com.br/Trebiann/getfristtwo.jsp");
		yield return www;

		string aux = www.text;
		string [] values  = aux.Split( new char [] {';'});

		assLeftName = values [1].Trim();
		assRightName = values [2].Trim();

		StartCoroutine (SetAssLeft(assLeftName));
		StartCoroutine (SetAssRight (assRightName));
		www.Dispose ();
	}

	private IEnumerator SetAssLeft(string ass){

		Image img = assLeft.GetComponent<Image>();
		img.sprite = loading;

		WWW www = new WWW("http://schumaker.com.br/App/photos/"+ass+".jpg");
		yield return www;

		Rect rec = new Rect(0, 0, www.texture.width, www.texture.height);
		Sprite spriteToUse = Sprite.Create(www.texture,rec,new Vector2(0.5f,0.5f),100);
		www.Dispose ();
		Image buttonImage = assLeft.GetComponent<Image>();
		buttonImage.sprite = spriteToUse;
	}

	private IEnumerator SetAssRight(string ass){

		Image img = assRight.GetComponent<Image>();
		img.sprite = loading;

		WWW www = new WWW("http://schumaker.com.br/App/photos/"+ass+".jpg");
		yield return www;

		Rect rec = new Rect(0, 0, www.texture.width, www.texture.height);
		Sprite spriteToUse = Sprite.Create(www.texture,rec,new Vector2(0.5f,0.5f),100);
		www.Dispose ();
		Image buttonImage = assRight.GetComponent<Image>();
		buttonImage.sprite = spriteToUse;
	}

	private IEnumerator SetScoreWinner(string id){
		WWW www = new WWW("http://schumaker.com.br/Trebiann/setwinner.jsp?w="+id+"");
		yield return www;  
	}

	private IEnumerator SetScoreLoser(string id){
		WWW www = new WWW("http://schumaker.com.br/Trebiann/setloser.jsp?l="+id+"");
		yield return www;   
	}

	public void Back(){
		HsAdmob.instance.RemoveBanners ();
		SceneManager.LoadScene ("_MainMenu");
	}
}