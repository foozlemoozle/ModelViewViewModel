/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/21/2021

using UnityEngine;
using com.keg.mvvm.model;
using com.keg.mvvm.util;

namespace com.keg.mvvm.viewmodel
{
    /// <summary>
	/// Base class for scriptable object styled translators.
	/// Building a translator as a scriptable object can make hooking them up in unity easier.
	/// </summary>
	/// <typeparam name="IN">Type of data that goes into the translator.</typeparam>
	/// <typeparam name="OUT">End result of the translated data.</typeparam>
    public abstract class TranslatorSO<IN, OUT> : TranslatorSO, ITranslator<IN, OUT>
    {
        public abstract void Translate( IN input );

        public virtual event System.Action<OUT> OnDataRetrieved;

		public sealed override void GetData()
		{
			IN raw = DataLayer.Query<IN>( Uri );
			Translate( raw );
		}
	}

    public abstract class TranslatorSO : ScriptableObject, ITranslator
	{
		public abstract void GetData();

        public IDataLayer DataLayer { get; set; }

        public URI Uri
        {
            get => _uri;
            set => _uri = value;
        }
        [SerializeField] private URI _uri;
	}
}
