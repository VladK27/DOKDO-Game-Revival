using System.Collections.Generic;
using SA.Common.Models;

public class MP_MediaPickerResult : Result
{
	private List<MP_MediaItem> _SelectedmediaItems;

	public List<MP_MediaItem> SelectedmediaItems
	{
		get
		{
			return _SelectedmediaItems;
		}
	}

	public List<MP_MediaItem> Items
	{
		get
		{
			return SelectedmediaItems;
		}
	}

	public MP_MediaPickerResult(List<MP_MediaItem> selectedItems)
	{
		_SelectedmediaItems = selectedItems;
	}

	public MP_MediaPickerResult(string errorData)
		: base(new Error(errorData))
	{
	}
}
