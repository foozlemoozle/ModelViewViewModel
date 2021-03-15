/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/22/2021

using System;
using UnityEngine;

namespace com.keg.mvvm.viewmodel
{
	[CreateAssetMenu( fileName = "StringToStringTranslator", menuName = "Translators/StringToString" )]
	public class StringToStringTranslator : TranslatorSO<string, string>
    {
		public override event Action<string> OnDataRetrieved;

		public override void Translate( string input )
		{
			OnDataRetrieved( input );
		}
	}
}
