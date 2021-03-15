/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/21/2021

using com.keg.mvvm.model;
using com.keg.mvvm.util;

namespace com.keg.mvvm.viewmodel
{
    /// <summary>
	/// Base class implementation for a translator.
	/// Can be used if instead of TranslatorSO if you don't want a scriptable object implementation.
	/// </summary>
	/// <typeparam name="IN">Type of data that goes into the translator.</typeparam>
	/// <typeparam name="OUT">End result of the translated data.</typeparam>
    public abstract class TranslatorBase<IN, OUT> : ITranslator<IN, OUT>
    {
        public abstract void Translate( IN input );

        public event System.Action<OUT> OnDataRetrieved;

        public IDataLayer DataLayer { get; set; }

        public URI Uri { get; set; }

		void ITranslator.GetData()
		{
			IN raw = DataLayer.Query<IN>( Uri );
			Translate( raw );
		}

		public virtual void Teardown()
		{
		}
    }
}
