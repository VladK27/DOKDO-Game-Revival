using SA.Common.Models;
using UnityEngine;

public class AN_PlusShareListener : MonoBehaviour
{
	public delegate void PlusShareCallback(Result result);

	private PlusShareCallback builderCallback = delegate
	{
	};

	public void AttachBuilderCallback(PlusShareCallback callback)
	{
		builderCallback = callback;
	}

	private void OnPlusShareCallback(string res)
	{
		Result result = ((!bool.Parse(res)) ? new Result(new Error()) : new Result());
		builderCallback(result);
	}
}
