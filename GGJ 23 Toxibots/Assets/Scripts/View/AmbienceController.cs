using Assets.Scripts;
using UnityEngine;

public class AmbienceController : MonoBehaviour, IMeterListener, ILevelManagerListener
{
	private static Color _TRUNK_01_COLOR = new Color(238.0f / 255.0f, 123 / 255.0f, 62 / 255.0f);
	private static Color _TRUNK_02_COLOR = new Color(233.0f / 255.0f, 103 / 255.0f, 41 / 255.0f);

	private static Color _FOLIAGE_01_COLOR_A = new Color(53.0f / 255.0f, 106 / 255.0f, 14 / 255.0f);
	private static Color _FOLIAGE_02_COLOR_A = new Color(196.0f / 255.0f, 209 / 255.0f, 35 / 255.0f);
	private static Color _FOLIAGE_03_COLOR_A = new Color(116.0f / 255.0f, 137 / 255.0f, 39 / 255.0f);
	private static Color _FOLIAGE_03_COLOR_B = new Color(72.0f / 255.0f, 99 / 255.0f, 15 / 255.0f);

	private static Color _FLOOR_COLOR = new Color(116.0f / 255.0f, 255 / 255.0f, 101 / 255.0f);

	[SerializeField]
	private Material _TrunkMat_01;

	[SerializeField]
	private Material _TrunkMat_02;

	[SerializeField]
	private Material _Foliage_01;

	[SerializeField]
	private Material _Foliage_02;

	[SerializeField]
	private Material _Foliage_03;

	[SerializeField]
	private GameObject _Floor;

	public void OnLevelChanged(string levelName)
	{
		ResetColors();
	}

	public void OnLevelCompleted(string levelName)
	{ }

	public void OnLevelFail(string levelName)
	{ }

	public void OnLevelManagerStateChanged(LevelState state)
	{ }

	public void OnLevelPaused(string levelName)
	{ }

	public void OnLevelReset(string levelName)
	{ }

	public void OnLevelResumed(string levelName)
	{ }

	public void OnLevelSequenceCompleted(string levelName)
	{
		//ResetColors();
	}

	public void OnLevelStarted(string levelName)
	{
		ResetColors();
	}

	public void OnMeterAmountChanged(float meterAmount)
	{
		_Floor.GetComponent<Renderer>().material.color = Color.Lerp(
			_FLOOR_COLOR,
			Color.black,
			meterAmount / 100.0f
		);

		_TrunkMat_01.color = Color.Lerp(
			_TRUNK_01_COLOR,
			Color.black,
			meterAmount / 100.0f
		);
		_TrunkMat_02.color = Color.Lerp(
			_TRUNK_02_COLOR, 
			Color.black, 
			meterAmount / 100.0f
		);
		_Foliage_01.SetColor("_Color", Color.Lerp(
			_FOLIAGE_01_COLOR_A,
			Color.red,
			meterAmount / 100.0f
		));
		_Foliage_02.SetColor("_Color", Color.Lerp(
			_FOLIAGE_02_COLOR_A,
			Color.red,
			meterAmount / 100.0f
		));
		_Foliage_03.SetColor("_Color", Color.Lerp(
			_FOLIAGE_03_COLOR_A,
			Color.red,
			meterAmount / 100.0f
		));
		_Foliage_03.SetColor("_Fresnel_Color", Color.Lerp(
			_FOLIAGE_03_COLOR_B,
			Color.red,
			meterAmount / 100.0f
		));
	}

	void Start()
    {
		ResetColors();
		GameManager.GetInstance().LevelManager.Subscribe(this);
		GameManager.GetInstance().LevelManager.Meter.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    { }

	void OnApplicationQuit()
	{
		ResetColors();
	}

	private void ResetColors()
	{
		_TrunkMat_01.color = _TRUNK_01_COLOR;
		_TrunkMat_02.color = _TRUNK_02_COLOR;
		_Foliage_01.SetColor("_Color", _FOLIAGE_01_COLOR_A);
		_Foliage_02.SetColor("_Color", _FOLIAGE_02_COLOR_A);
		_Foliage_03.SetColor("_Color", _FOLIAGE_03_COLOR_A);
		_Foliage_03.SetColor("_FrenelColor", _FOLIAGE_03_COLOR_B);
		_Floor.GetComponent<Renderer>().material.color = _FLOOR_COLOR;
	}
}
