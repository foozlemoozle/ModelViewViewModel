/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/28/2021

using UnityEngine;

namespace com.keg.mvvm.tests.model
{
	public class SimpleData : PrefabDataModel
	{
		[SerializeField] private string _stringData;
		[SerializeField] private int _intData;
		[SerializeField] [Range( 0f, 1f )] private float _floatData;

		public override T GetData<T>( string key )
		{
			object downcasted;

			switch( key.ToUpper() )
			{
			case "STRINGDATA":
				downcasted = _stringData;
				break;
			case "INTDATA":
				downcasted = _intData;
				break;
			case "FLOATDATA":
				downcasted = _floatData;
				break;
			default:
				throw new System.MissingFieldException( $"Field {key} case insensitive does not exist, or is not implemented" );
			}

			return (T)downcasted;
		}
	}
}
