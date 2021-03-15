/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/21/2021

using UnityEngine;
using com.keg.mvvm.viewmodel;
using com.keg.mvvm.util;

namespace com.keg.mvvm.view
{
	/// <summary>
	/// Binds a gameobject to a translator.
	/// Should update visuals with data when as soon as possible, and whenever data is updated.
	/// Intended to extend a Monobehaviour class.
	/// Portion of the IViewBinding duopoly that governs binding to an external component.
	/// </summary>
	/// <typeparam name="BINDING">Component this is bound to.</typeparam>
	public interface IViewBinding<BINDING, OUT> : IViewBinding<OUT> where BINDING : Component
	{
		/// <summary>
		/// View component this is bound to.
		/// If this implements a monobehaviour, it's recommended that BINDING be a required component.
		/// </summary>
		BINDING Binding { get; }
	}

	/// <summary>
	/// Binds a gameobject to a translator.
	/// Should update visuals with data when as soon as possible, and whenever data is updated.
	/// Portion of the IViewBinding duopoly that governs translator interactions.
	/// </summary>
	/// <typeparam name="OUT">Translator data output--data type desired by this view.</typeparam>
	public interface IViewBinding<OUT>
	{
		/// <summary>
		/// Uri to query the translator with for data.
		/// Should probably be serialized on the monobehaviour implementation.
		/// </summary>
		/// TODO: Should this serialize URI?  I think it's better for the translator to own it
		//URI Uri { get; }

		/// <summary>
		/// Translator used to interface with IDataLayer.
		/// </summary>
		ITranslator<OUT> Translator { get; }

		/// <summary>
		/// Call that occurs when data is updated.
		/// This should refresh the view to show the new data.
		/// This should be registered to ITranslator::OnDataRetrieved event.
		/// </summary>
		/// <param name="translatedData">Data to render.</param>
		void OnDataUpdated( OUT translatedData );
	}
}
