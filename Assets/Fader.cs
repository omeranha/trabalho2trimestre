using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Fader : MonoBehaviour
{
	private Image image;
	public float fadeSpeed = 1.5f;

	private void Start()
	{
		image = GetComponent<Image>();
		StartCoroutine(FadeIn());
	}

	public void FadeScene(string sceneName)
	{
		StartCoroutine(FadeOut(sceneName));
	}

	IEnumerator FadeIn()
	{
		float alpha = image.color.a;
		while (alpha > 0f) {
			alpha -= Time.deltaTime * fadeSpeed;
			image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
			yield return null;
		}
	}

	IEnumerator FadeOut(string sceneName)
	{
		float alpha = image.color.a;
		while (alpha < 1f) {
			alpha += Time.deltaTime * fadeSpeed;
			image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
			yield return null;
		}
		SceneManager.LoadScene(sceneName);
	}
}
