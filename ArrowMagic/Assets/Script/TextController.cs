using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour
{
	public GameObject TutorialUI;

	[SerializeField]
	Text uiText;

	//　読む込むテキストが書き込まれている.txtファイル
	[SerializeField]
	private TextAsset textAsset;
	//　テキストファイルから読み込んだデータ
	private string loadText1;
	//　改行で分割して配列に入れる
	private string[] splitText1;
	//　現在表示中テキスト1番号
	public static int textNum1;

	[SerializeField]
	[Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;

	private string currentText = string.Empty;
	private float timeUntilDisplay = 0;
	private float timeElapsed = 1;
	private int lastUpdateCharacter = -1;
	bool iti = true;
	bool niyon = true;

	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText
	{
		get { return Time.time > timeElapsed + timeUntilDisplay; }
	}

	void Start()
	{
		loadText1 = textAsset.text;
		splitText1 = loadText1.Split(char.Parse("\n"));
		if (SceneManager.GetActiveScene().name == "Stage1") textNum1 = 0;
		else if (SceneManager.GetActiveScene().name == "Stage3") textNum1 = 7;
		TutorialUI.SetActive(true);
		SetNextLine();
	}

	void Update()
	{
		FinishText();

		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		if (displayCharacterCount != lastUpdateCharacter)
		{
			uiText.text = currentText.Substring(0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}


	public void SetNextLine()
	{
		currentText = splitText1[textNum1];
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		textNum1++;
		lastUpdateCharacter = -1;
		Debug.Log("シナリオ" + textNum1);
	}

	void FinishText()
	{
		if (textNum1 == 6 || textNum1 == 11) TutorialUI.SetActive(false);
	}

	public void OnSkip()
	{
		TutorialUI.SetActive(false);
	}

	public void OnContinueReading()
	{
		// 文字の表示が完了してるならクリック時に次の行を表示する
		if (IsCompleteDisplayText)
		{
			if (textNum1 < splitText1.Length)
			{
				SetNextLine();
			}
		}
		else
		{
			// 完了してないなら文字をすべて表示する

			timeUntilDisplay = 0;
		}
	}
}