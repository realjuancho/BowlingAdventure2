using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionGroup : MonoBehaviour {

	SelectableItem[] Items;
	SettingsManager settingsManager;

	// Use this for initialization
	void Start () {


		settingsManager = GameObject.FindObjectOfType<SettingsManager>();

		Items = GetComponentsInChildren<SelectableItem>();


	}
	
	// Update is called once per frame
	void Update () {

		foreach(SelectableItem sI in Items)
		{
			if(sI.isSelected == SelectionManager.Selected.Yes)
			{
				settingsManager.setGameSetting(sI.setting, sI.settingValue);
				break;
			}
		}

	}
}
