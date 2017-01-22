using UnityEngine;
using System.Collections;

public class SelectablePage : MonoBehaviour {


	public Transform selectedCameraPosition;

	public SelectionManager.Selected isSelected;

	public SelectablePage NextPage;
	public SelectablePage PreviousPage;


	public float selectSpeed= 3.0f;


	void Start () {

}
	
	// Update is called once per frame
	void Update () {


	}




	public void SelectPage()
	{
		isSelected  = SelectionManager.Selected.Yes;
	}

	public void DeSelectPage()
	{
		isSelected = SelectionManager.Selected.No;
	}


}
