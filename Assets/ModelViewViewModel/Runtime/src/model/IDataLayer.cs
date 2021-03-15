/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/21/2021

using System.Collections.Generic;
using com.keg.mvvm.util;
using com.keg.mvvm.viewmodel;

namespace com.keg.mvvm.model
{
    /// <summary>
	/// Access point for the data layer.
	/// Reports data to translators.
	/// </summary>
    public interface IDataLayer : IExplicitQueryableData
    {
        event System.Action OnDataUpdated;
    }
}
