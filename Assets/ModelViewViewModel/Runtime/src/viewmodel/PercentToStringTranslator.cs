/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/28/2021

using System;
using UnityEngine;

namespace com.keg.mvvm.viewmodel
{
	[CreateAssetMenu( fileName = "PercentToStringTranslator", menuName = "Translators/PercentToString" )]
	public class PercentToStringTranslator : TranslatorSO<float, string>
    {
		public override event Action<string> OnDataRetrieved;

		public override void Translate( float input )
		{
			OnDataRetrieved( $"{input * 100f}%" );
		}
	}
}
