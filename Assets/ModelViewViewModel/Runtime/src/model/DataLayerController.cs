/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/21/2021

using com.keg.mvvm.util;

namespace com.keg.mvvm.model
{
    /// <summary>
	/// Manager class for Model View View-Model.
	/// Access point for View-Model to access model code.
	/// </summary>
    public abstract class DataLayerController : IDataLayer
    {
		public abstract event System.Action OnDataUpdated;
        public abstract T GetData<T>( string key );
    }
}
