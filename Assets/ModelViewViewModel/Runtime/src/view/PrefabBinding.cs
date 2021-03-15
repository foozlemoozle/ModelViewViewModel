/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/22/2021

using UnityEngine;

namespace com.keg.mvvm.view
{
	/// <summary>
	/// Binding for loading a prefab at the specified transform.
	/// </summary>
    public class PrefabBinding : ViewBindingSOBase<Transform, Transform>
    {
		public override void OnDataUpdated( Transform data )
		{
			data.SetParent( _binding, false );
			data.localPosition = Vector3.zero;
			data.localRotation = Quaternion.identity;
		}
	}
}
