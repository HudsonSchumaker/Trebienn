using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* 
	@author Hudson Schumaker
	@version 1.0.0
*/
public class HsGetImage : MonoBehaviour {

	public string url = "http://schumakerteam.com/officehero/photos/1.jpg";
	public GameObject obj;

	IEnumerator Start()
	{
		// Start a download of the given URL
		WWW www = new WWW(url);

		// Wait for download to complete
		yield return www;

		// assign texture
		Rect rec = new Rect(0, 0, www.texture.width, www.texture.height);
		Sprite spriteToUse = Sprite.Create(www.texture,rec,new Vector2(0.5f,0.5f),100);

		Image buttonImage = this.gameObject.GetComponent<Image>();
		buttonImage.sprite = spriteToUse;
	}
}
