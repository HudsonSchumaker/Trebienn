using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
	@author Hudson Schumaker
	@version 1.0.0

*/

public class HsSplashScreen : MonoBehaviour {

	private SpriteRenderer grafico;
	private float larguraImagem;
	private float alturaImagem;
	private float alturaTela;
	private float larguraTela;
	private float delay;

	private void Start () {
		this.delay = 3.56789f;
	
		this.grafico = GetComponent<SpriteRenderer> ();
		this.larguraImagem = grafico.sprite.bounds.size.x;
		this.alturaImagem = grafico.sprite.bounds.size.y;
		this.alturaTela = Camera.main.orthographicSize * 2.0f;
		this.larguraTela = alturaTela / Screen.height * Screen.width;
		Vector2 novaEscala = transform.localScale;
		novaEscala.x = larguraTela/larguraImagem;
		novaEscala.y = alturaTela/alturaImagem;
		this.transform.localScale = novaEscala;
	}

	private void Update () {
		delay -= Time.deltaTime;
		if(delay <= 0.0f){
			SceneManager.LoadScene("_MainMenu");
		}
	}
}
