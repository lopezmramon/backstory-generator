using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System.Collections;

public class StoryUIController : MonoBehaviour
{
	public StoryGenerator storyGenerator;
	public TextMeshProUGUI storyDisplay;
	public GameObject[] toggleWhenFirstGrammarIsGenerated;
	[TextArea]
	public string grammar;
	public TMP_InputField ageInput, nameInput, lastNameInput;
	public TMP_Dropdown genderDropdown, locationDropdown;
	public Sprite[] males, females, locations, storyHooks;
	public Image character, location;
	public Image[] storyHookDisplays;
	public List<Sprite> castleSprites, forestSprites, farBerrySprites;
	private AudioSource audioSource;
	public AudioClip[] typeClips;
	private WaitForSeconds typeWait, commaTypeWait, periodTypeWait;
	public float typeWaitSeconds;

	private void Awake()
	{
		GenerateGrammar();
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.volume = 0.75f;
		typeWait = new WaitForSeconds(typeWaitSeconds);
		commaTypeWait = new WaitForSeconds(typeWaitSeconds * 2.5F);
		periodTypeWait = new WaitForSeconds(typeWaitSeconds * 10f);
		storyGenerator = new StoryGenerator(grammar);
		SetupInputs();
		ToggleObjects(false);
	}

	private void SetupInputs()
	{
		locationDropdown.ClearOptions();
		locationDropdown.AddOptions(Enum.GetNames(typeof(Location)).ToList());
		ageInput.onValueChanged.AddListener((x) => OnInputChanged());
		nameInput.onEndEdit.AddListener((x) => OnInputChanged());
		lastNameInput.onEndEdit.AddListener((x) => OnInputChanged());
		genderDropdown.onValueChanged.AddListener((x) => OnInputChanged());
		locationDropdown.onValueChanged.AddListener((x) => OnInputChanged());
	}

	private void OnInputChanged()
	{
		if (nameInput.text == string.Empty || lastNameInput.text == string.Empty)
		{
			return;
		}
		GenerateGrammar();
		storyGenerator.UpdateGrammar(grammar);
		DisplayGrammar();
	}

	public void GenerateGrammar()
	{
		string nameRule = nameInput.text.Length > 0 ? "#name.capitalize#" : "#name#";
		string lastNameRule = lastNameInput.text.Length > 0 ? "#lastName.capitalize#" : "#lastName#";
		grammar = "{" +
			$"\"intro\":[\"Once upon a time\" , \"You know, I love old stories. Let me tell you the story of\" , " +
   $"\"I'm trying to remember that old story, I think it was\"]," +
			$"\"name\": [\"{nameInput.text}\"], " +
			$"\"lastName\": [\"{lastNameInput.text}\"], " +
			$"\"gender\": [\"{GenderHelper.GetEnumDescription((Gender)genderDropdown.value)}\"]," +
			$"\"location\": [\"{EnumHelper.GetEnumDescription((Location)locationDropdown.value)}\"]," +
			$"\"areaIntro\":[\"{EnumHelper.GetEnumDescription((AreaBeginnings)locationDropdown.value)}\"]," +
			$"\"bigEvent\":[\"{EnumHelper.GetEnumDescription((AreaBigEvent)locationDropdown.value)}\"]," +
			$"\"areaDevelopment\":[\"{EnumHelper.GetEnumDescription((AreaDevelopment)locationDropdown.value)}\"]," +
			$"\"currentState\":[\"{EnumHelper.GetEnumDescription((AreaCurrentState)locationDropdown.value)}\"]," +
			$"\"mediumEvent\":[\"{EnumHelper.GetEnumDescription((AreaMediumEvent)locationDropdown.value)}\"]," +
			$"\"areaEnd\":[\"{EnumHelper.GetEnumDescription((AreaEnd)locationDropdown.value)}\"]," +
			$"\"callToAdventure\":[\"{EnumHelper.GetEnumDescription((CallToAdventure)locationDropdown.value)}\"]," +
			$"\"born\": [\" born in #location#\", \" born around #location#\", \" native to #location#\",\" raised in #location#\"]," +
			$"\"origin\": [\"#intro# [hero:{nameRule}]#hero#" +
						 $" {lastNameRule}," +
						 $"#born##areaIntro##bigEvent##areaDevelopment##mediumEvent#" +
						 $"#currentState##areaEnd##callToAdventure#.\"]" + "}";
	}

	private void ToggleObjects(bool active)
	{
		foreach(GameObject gO in toggleWhenFirstGrammarIsGenerated)
		{
			gO.SetActive(active);
		}
	}

	public void DisplayGrammar()
	{
		StopAllCoroutines();
		ShowExtraVisuals();
		ToggleObjects(true);
		StartCoroutine(TypeText(storyGenerator.GenerateStory()));
		//	storyDisplay.text = storyGenerator.GenerateStory();
	}

	private void ShowExtraVisuals()
	{
		character.color = Color.white;
		character.GetComponent<Outline>().enabled = true;
		location.color = Color.white;
		location.preserveAspect = true;		
		RerollCharacter();
		RerollLocation();
		RerollHooks();
	}

	public void RerollHooks()
	{
		foreach(Image image in storyHookDisplays)
		{
			int randomHook = UnityEngine.Random.Range(0, storyHooks.Length);
			image.sprite = storyHooks[randomHook];
		}
	}

	public void RerollGrammar()
	{
		GenerateGrammar();
		DisplayGrammar();
	}

	public void RerollCharacter()
	{
		character.sprite = (Gender)genderDropdown.value == Gender.Male ? males[UnityEngine.Random.Range(0, males.Length)]
		: females[UnityEngine.Random.Range(0, females.Length)];
	}

	public void RerollLocation()
	{
		Sprite locationSprite = null;
		switch ((Location)locationDropdown.value)
		{
			case Location.NorekiForest:
				locationSprite = forestSprites[UnityEngine.Random.Range(0, forestSprites.Count)];
				break;
			case Location.Castle:
				locationSprite = castleSprites[UnityEngine.Random.Range(0, castleSprites.Count)];
				break;
			case Location.FarBerry:
				locationSprite = farBerrySprites[UnityEngine.Random.Range(0, castleSprites.Count)];
				break;
		}
		location.sprite = locationSprite;
	}

	private IEnumerator TypeText(string story)
	{
		storyDisplay.text = string.Empty;
		int letterCount = 0;
		foreach (char letter in story.ToCharArray())
		{
			storyDisplay.text += letter;
			letterCount++;
			if (letterCount % 20 == 0)
			{
				if (typeClips[1])
					audioSource.PlayOneShot(typeClips[1], 0.1f);
			}
			if (letterCount % 2 == 0)
			{
				int randomClip = UnityEngine.Random.Range(0, typeClips.Length);
				if (randomClip == 1) randomClip = 0;
				if (typeClips[randomClip])
					audioSource.PlayOneShot(typeClips[randomClip], 0.1f);
			}

			if (letter.ToString() == ".")
			{
				yield return periodTypeWait;
			}
			else if (letter.ToString() == "," || letter.ToString() == ";")
			{
				yield return commaTypeWait;
			}
			else
			{
				yield return typeWait;
			}
		}
	}
}
