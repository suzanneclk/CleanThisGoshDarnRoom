using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ImageAnimation : MonoBehaviour {

	public Sprite[] sprites;
	public int spritePerFrame = 1;
	public bool loop = true;
	public bool destroyOnEnd = false;

	private int index = 0;
	private Image image;
	private int frame = 0;

	void Awake() {
		image = GetComponent<Image> ();
	}

	void Update () {
		if (!loop && index == sprites.Length) return;
		frame ++;
		if (frame < spritePerFrame) return;
		image.sprite = sprites [index];
		frame = 0;
		index ++;
		if (index >= sprites.Length) {
			if (loop) index = 0;
			if (destroyOnEnd) SceneManager.LoadScene(1); //I changed this line bc the code just played the animation every time I went to the main menu
		}
    }
}