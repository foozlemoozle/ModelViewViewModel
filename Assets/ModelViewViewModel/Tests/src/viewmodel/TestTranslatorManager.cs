/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/28/2021

using UnityEngine;
using com.keg.mvvm.viewmodel;
using com.keg.mvvm.model;
using com.keg.mvvm.tests.model;

namespace com.keg.mvvm.tests.viewmodel
{
    public class TestTranslatorManager : MonoBehaviour, ITranslatorManager
    {
        IDataLayer ITranslatorManager.DataLayer { get; set; }

        // Start is called before the first frame update
        void Awake()
        {
            this.Initialize( GameObject.FindObjectOfType<TestDataLayer>() );
        }

        void OnDestroy()
		{
            this.Teardown();
		}
    }
}
