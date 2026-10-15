public class AMN_AdProperties
{
	private bool _canExpand;

	private bool _canPlayAudio;

	private bool _canPlayVideo;

	private string _adtype;

	public bool CanExpand
	{
		get
		{
			return _canExpand;
		}
		set
		{
			_canExpand = value;
		}
	}

	public bool CanPlayAudio
	{
		get
		{
			return _canPlayAudio;
		}
		set
		{
			_canPlayAudio = value;
		}
	}

	public bool CanPlayVideo
	{
		get
		{
			return _canPlayVideo;
		}
		set
		{
			_canPlayVideo = value;
		}
	}

	public string Adtype
	{
		get
		{
			return _adtype;
		}
		set
		{
			_adtype = value;
		}
	}

	public AMN_AdProperties(bool canExpand, bool canPlayAudio, bool canPlayVideo, string adtype)
	{
		CanExpand = canExpand;
		CanPlayAudio = canPlayAudio;
		CanPlayVideo = canPlayVideo;
		Adtype = adtype;
	}
}
