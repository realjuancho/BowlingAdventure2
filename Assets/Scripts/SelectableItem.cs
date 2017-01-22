using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxCollider))]
public class SelectableItem : MonoBehaviour {


	public bool isEnabled = true;
	
	public SelectionManager.Selected isSelected;
	public SelectionManager.Hovering isHovering;
	public bool staySelected;


	public SelectionManager.SelectionType selectionType;
	public string SelectionGroup;

	public Material SelectedItemMaterial;
	public Material NormalItemMaterial;

	public SelectablePage GoToPage;

	public SettingsManager.SettingMenu setting;
	public string settingValue;

	Animation[] animationItemsFromChildren;
	MeshRenderer[] meshRenderersFromChildren;


	// Use this for initialization
	void Start () {

		animationItemsFromChildren = GetComponentsInChildren<Animation>();
		meshRenderersFromChildren = GetComponentsInChildren<MeshRenderer>();

	}


	bool finishedSelection;
	// Update is called once per frame
	void Update () {
		ListenSelection();
		ListenHovering();

		if(finishedSelection)
			finishedSelection = false;
	}

	void ListenHovering()
	{
		if(isHovering == SelectionManager.Hovering.Yes)
		{
			
			foreach(Animation a in animationItemsFromChildren)
			{
				a.enabled = true;

			}
				
		}
		else 
		{
			foreach(Animation a in animationItemsFromChildren)
			{

				if(a.clip)
					a[a.clip.name].normalizedTime = 0.0f;

				a.enabled = false;

			}
		}
	}


	bool playedAnimation;
	void ListenSelection()
	{

		if(!playedAnimation)
		if(isSelected == SelectionManager.Selected.Yes)
		{
			Animation anim = GetComponent<Animation>();
			if(anim)
			{
				anim.wrapMode = WrapMode.Once;
				anim.Play("Select");
				playedAnimation = true;
			}
			if(SelectedItemMaterial)
			{
				foreach(MeshRenderer a in meshRenderersFromChildren)
				{
					a.material = SelectedItemMaterial;
				}
			}

		}
		if (isSelected == SelectionManager.Selected.No)
		{
			playedAnimation = false;

			if(NormalItemMaterial)
			{
				foreach(MeshRenderer a in meshRenderersFromChildren)
				{
					
					a.material = NormalItemMaterial;
				}
			}
		}
	
	}

	public void SelectedItem()
	{
		isSelected = SelectionManager.Selected.Yes;

		if(selectionType == SelectionManager.SelectionType.MoveNextPage)
		{
			
		}
	}

	public void HoveringItem()
	{
		isHovering = SelectionManager.Hovering.Yes;
	}

	public void UnSelectItem()
	{
		isSelected = SelectionManager.Selected.No;
	}

	public void LeaveHovering()
	{
		isHovering = SelectionManager.Hovering.No;
	}



	public void ClickSelect()
	{
		finishedSelection = true;
	}

}
