/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/22/2021

using UnityEngine;
using UnityEngine.UI;

namespace com.keg.mvvm.view
{
	/// <summary>
	/// Binding for an image game object.  
	/// </summary>
	public class ImageBinding : ViewBindingSOBase<Image, Sprite>
	{
		public override void OnDataUpdated( Sprite data )
		{
			_binding.sprite = data;
		}
	}
}
