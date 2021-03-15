/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/28/2021

using System;
using UnityEngine;

namespace com.keg.mvvm.viewmodel
{
	[CreateAssetMenu( fileName = "IntToStringTranslator", menuName = "Translators/IntToString" )]
	public class IntToStringTranslator : TranslatorSO<int, string>
	{
		public override event Action<string> OnDataRetrieved;

		public override void Translate( int input )
		{
			OnDataRetrieved( input.ToString() );
		}
	}
}
