using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryHooksController : MonoBehaviour
{
	private Button button;
	private HorizontalLayoutGroup layoutGroup;

	private void Awake()
	{
		button = GetComponent<Button>();
		layoutGroup = GetComponent<HorizontalLayoutGroup>();
		button.onClick.AddListener(() =>
		{
			ToggleCollapse();
		});
	}

	private void ToggleCollapse()
	{
		bool collapse = layoutGroup.spacing == 0;
		layoutGroup.spacing = collapse ? -150 : 0;
		foreach (Transform child in transform)
		{
			Vector3 eulerAngles = Vector3.zero;
			if (collapse)
			{
				if (child.GetSiblingIndex() == 0)
				{
					eulerAngles = new Vector3(0, 0, 20);
				}
				else if (child.GetSiblingIndex() == 2)
				{
					eulerAngles = new Vector3(0, 0, -20);
				}
			}
			child.eulerAngles = eulerAngles;
		}
	}
}
