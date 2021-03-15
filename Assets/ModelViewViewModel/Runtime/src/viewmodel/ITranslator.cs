/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/21/2021

using com.keg.mvvm.model;
using com.keg.mvvm.view;
using com.keg.mvvm.util;

namespace com.keg.mvvm.viewmodel
{
	/// <summary>
	/// Translator interface.
	/// Portion of ITranslator that defines Model interactions.  
	/// Translators are used to modify the raw model data into a form that you want to present.
	/// </summary>
	/// <typeparam name="IN">Type of data that goes into the translator.</typeparam>
	/// <typeparam name="OUT">End result of the translated data.</typeparam>
	public interface ITranslator<IN, OUT> : ITranslator<OUT>
    {
		/// <summary>
		/// Translates IN data into OUT format.
		/// Should fire the ITranslator<OUT>::OnDataRetrieved event after translation is complete.
		/// </summary>
		/// <param name="input">Data to translate.</param>
		/// <returns></returns>
        void Translate( IN input );
	}

	/// <summary>
	/// Translator interface.
	/// Portion of the ITranslator that defines view interactions.
	/// </summary>
	/// <typeparam name="OUT"></typeparam>
	public interface ITranslator<OUT> : ITranslator
	{
		/// <summary>
		/// Event that fires once Get has retrieved and translated data.
		/// Should fire OnDataRetrieved after translation.
		/// </summary>
		event System.Action<OUT> OnDataRetrieved;
	}

	/// <summary>
	/// Translator interface.
	/// Portion of the ITranslator that defines basic accessors. 
	/// </summary>
	public interface ITranslator
	{
		/// <summary>
		/// Reference to the data layer.
		/// Queried to retrieve data to translate.
		/// </summary>
		IDataLayer DataLayer { get; set; }

		/// <summary>
		/// URI for query the data layer.  
		/// </summary>
		URI Uri { get; set; }

		/// <summary>
		/// Retrieves data from data layer.
		/// Calls Translate internally.
		/// Entry point for the view to retrieve data.
		/// </summary>
		void GetData();
	}

	public static class TranslatorImplementation
	{
		/// <summary>
		/// Attaches the translator to the data layer.
		/// Registers for IDataLayer::OnDataUpdated.
		/// </summary>
		/// <param name="translator">Translator to attach to data layer.</param>
		/// <param name="dataLayer">DataLayer to be queried.</param>
		public static void AttachToDataLayer( this ITranslator translator, IDataLayer dataLayer)
		{
			translator.DataLayer = dataLayer;
			dataLayer.OnDataUpdated += translator.GetData;
		}

		/// <summary>
		/// Detaches the translator from the data layer.
		/// </summary>
		/// <param name="translator">Translator to detach from data layer.</param>
		public static void DetachFromDataLayer( this ITranslator translator )
		{
			translator.DataLayer.OnDataUpdated -= translator.GetData;
			translator.DataLayer = null;
		}
	}
}
