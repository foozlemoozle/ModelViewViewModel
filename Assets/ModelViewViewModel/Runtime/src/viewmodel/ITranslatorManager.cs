/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/28/2021

using com.keg.mvvm.model;
using com.keg.mvvm.view;

namespace com.keg.mvvm.viewmodel
{
    public interface ITranslatorManager
    {
        IDataLayer DataLayer { get; set; }
    }

    public static class TranslatorManagerImplementation
	{
        public static void Initialize( this ITranslatorManager translatorManager, IDataLayer dataLayer )
		{
            translatorManager.DataLayer = dataLayer;
            ViewBindingBase.OnTranslatorCreated += translatorManager.OnTranslatorCreated;
            ViewBindingBase.OnTranslatorDestroyed += translatorManager.OnTranslatorDestroyed;

        }

        public static void Teardown( this ITranslatorManager translatorManager )
		{
            translatorManager.DataLayer = null;
            ViewBindingBase.OnTranslatorCreated -= translatorManager.OnTranslatorCreated;
            ViewBindingBase.OnTranslatorDestroyed -= translatorManager.OnTranslatorDestroyed;
        }

        private static void OnTranslatorCreated( this ITranslatorManager translatorManager, ITranslator translator )
		{
            translator.AttachToDataLayer( translatorManager.DataLayer );
            translator.GetData();
		}

        private static void OnTranslatorDestroyed( this ITranslatorManager translatorManager, ITranslator translator )
		{
            translator.DetachFromDataLayer();
		}
	}
}
