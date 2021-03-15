/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/28/2021

using UnityEngine;
using System.Collections.Generic;
using com.keg.mvvm.model;

namespace com.keg.mvvm.tests.model
{
    public class TestDataLayer : MonoBehaviour, IDataLayer
    {
        [SerializeField] private List<DataTuple> _dataStore;

        public event System.Action OnDataUpdated;

        T IExplicitQueryableData.GetData<T>(string key)
		{
            object found = null;
            foreach( DataTuple tuple in _dataStore )
			{
                if( tuple.key == key )
				{
                    found = tuple.value;
				}
			}

            return (T)found;
		}

		public void Update()
		{
            OnDataUpdated();
		}
	}

    [System.Serializable]
    public struct DataTuple
    {
        public string key;
        public PrefabDataModel value;
    }
}
