using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxCollider))]
public class TouchButtons : MonoBehaviour {

	// Use this for initialization

	public string Name;

	public Material pushedButtonMaterial;
	Animator buttonAnimator;

	MeshRenderer meshRenderer;
	Material originalMaterial;

	bool _isTouched;


	void Start()
	{
		meshRenderer = GetComponent<MeshRenderer> ();

		if(meshRenderer)
		originalMaterial = meshRenderer.material;

		buttonAnimator = GetComponent<Animator>();
	}

	void Update()
	{
		CheckTouched ();
	}
		


	void CheckTouched()
	{
		if (_isTouched) {

			//Debug.Log (Name);


			if (pushedButtonMaterial)
				meshRenderer.material = pushedButtonMaterial;

			if (buttonAnimator)
				buttonAnimator.SetTrigger("Push");

		} else 
		{
			if(meshRenderer)
				meshRenderer.material = originalMaterial;
		}
		setTouched (false);
	}
		
	public void setTouched(bool isTouched)
	{
		_isTouched = isTouched;
	}

	public bool isTouched()
	{
		return _isTouched;
	}

}
