using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MeterTextView : MonoBehaviour , IMeterListener
{
	[SerializeField]
	public Text meterAmountText;

	public void OnMeterAmountChanged(float meterAmount)
	{
		if (meterAmountText != null)
			meterAmountText.text = meterAmount.ToString();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
