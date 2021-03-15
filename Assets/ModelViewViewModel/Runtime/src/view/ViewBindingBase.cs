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
	/// Base class implementation of IViewBinding.
	/// </summary>
	/// <typeparam name="BINDING">Component this is bound to.</typeparam>
	/// <typeparam name="OUT">ITranslator data output--type of data desired by this component.</typeparam>
    public abstract class ViewBindingBase<BINDING, OUT>
        : ViewBindingBase, IViewBinding<BINDING, OUT> where BINDING : Component
    {
		public abstract ITranslator<OUT> Translator { get; }
		public abstract void OnDataUpdated( OUT translatedData );

		[SerializeField] protected BINDING _binding;
		//[SerializeField] protected URI _dataUri;

		//URI IViewBinding<OUT>.Uri => _dataUri;

		BINDING IViewBinding<BINDING, OUT>.Binding
		{
			get
			{
				if( _binding == null )
				{
					_binding = this.GetComponent<BINDING>();
				}

				return _binding;
			}
		}

		protected virtual void Awake()
		{
			//Translator.Uri = _dataUri;
			Translator.OnDataRetrieved += OnDataUpdated;
		}

		protected virtual void OnDestroy()
		{
			Translator.OnDataRetrieved -= OnDataUpdated;
		}
	}

	public abstract class ViewBindingBase : MonoBehaviour
	{
		public static System.Action<ITranslator> OnTranslatorCreated;
		public static System.Action<ITranslator> OnTranslatorDestroyed;
	}

	/// <summary>
	/// Base class Implementation of ViewBindingBase that utilizes the scriptable object translator pattern.  
	/// </summary>
	/// <typeparam name="BINDING">Component this is bound to.</typeparam>
	/// <typeparam name="OUT">ITranslator data output--type of data desired by this component.</typeparam>
	public abstract class ViewBindingSOBase<BINDING, OUT>
		: ViewBindingBase<BINDING, OUT> where BINDING : Component
	{
		[SerializeField] private TranslatorSO _translator;

		public override ITranslator<OUT> Translator => _translator as ITranslator<OUT>;

		protected override void Awake()
		{
			base.Awake();
			ViewBindingBase.OnTranslatorCreated( _translator );
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			ViewBindingBase.OnTranslatorDestroyed( _translator );
		}
	}
}
