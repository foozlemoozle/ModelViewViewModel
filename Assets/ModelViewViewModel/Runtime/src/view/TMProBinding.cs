/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/22/2021

using TMPro;

namespace com.keg.mvvm.view
{
	/// <summary>
	/// Binding for a textmeshpro game object.  
	/// </summary>
	public class TMProBinding : ViewBindingSOBase<TextMeshProUGUI, string>
	{
		public override void OnDataUpdated( string data )
		{
			_binding.text = data;
		}
	}
}
