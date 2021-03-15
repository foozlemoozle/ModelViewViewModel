/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/28/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.keg.mvvm.model;

namespace com.keg.mvvm.tests.model
{
    public abstract class PrefabDataModel : MonoBehaviour, IExplicitQueryableData
    {
        public abstract T GetData<T>( string key );
    }
}
