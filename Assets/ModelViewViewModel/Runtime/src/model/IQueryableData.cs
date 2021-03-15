/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/28/2021

using com.keg.mvvm.util;
using System.Reflection;

namespace com.keg.mvvm.model
{
	/// <summary>
	/// Base interface for queryable data.
	/// Querying implementation handled by QueryableImplementation.
	/// </summary>
	public interface IQueryableData
	{
	}

	/// <summary>
	/// Interface for queryable data.
	/// Use this instead of IQueryableData if you don't want to access data through reflection.
	/// IQueryableData utilizes reflection.  
	/// </summary>
	public interface IExplicitQueryableData : IQueryableData
	{
		/// <summary>
		/// Retrieves data from this class.
		/// If data is not found, URI.InvalidURIPathException should be thrown.
		/// </summary>
		/// <typeparam name="T">Type data is expected to be in.</typeparam>
		/// <param name="key">Key to define what data to retrieve.</param>
		/// <returns>Value as expected type T.  If not found, throws URI.InvalidURIPathException</returns>
		T GetData<T>( string key );
	}

	public static class QueryableDataImplementation
	{
		/// <summary>
		/// Uses explicitly defined data retrieval to query for data.
		/// </summary>
		/// <typeparam name="T">Expected type of resulting data.</typeparam>
		/// <param name="data">Data object to query.</param>
		/// <param name="uri">Path to resulting data.</param>
		/// <returns>Value at given URI.  If not found, throws URI.InvalidURIPathException.</returns>
		public static T Query<T>(this IExplicitQueryableData data, URI uri)
		{
			string key = uri.TopDomain;

			URI deeper;
			if( uri.TryPop( out deeper ) )
			{
				IQueryableData queryableData = data.GetData<IQueryableData>( key );
				IExplicitQueryableData explicitQueryableData = queryableData as IExplicitQueryableData;
				if( explicitQueryableData != null )
				{
					return explicitQueryableData.Query<T>( deeper );
				}
				else
				{
					return queryableData.ReflectionQuery<T>( deeper );
				}
			}

			return data.GetData<T>( key );
		}

		/// <summary>
		/// Uses System.Reflection to query for data. 
		/// </summary>
		/// <typeparam name="T">Expected type of resulting data.</typeparam>
		/// <param name="data">Data object to query.</param>
		/// <param name="uri">Path to resulting data.  Path should be defined using field names.</param>
		/// <returns>Value at given URI.  Returns default if not found.</returns>
		public static T SafeReflectionQuery<T>(this IQueryableData data, URI uri)
		{
			try
			{
				return data.ReflectionQuery<T>( uri );
			}
			catch( URI.InvalidURIPathException )
			{
			}

			return default;
		}

		/// <summary>
		/// Uses System.Reflection to query for data.
		/// First attempts to find the data through fields.
		/// If this doesn't work, attempts to find data through properties.
		/// If neither works, an exception is thrown.
		/// </summary>
		/// <typeparam name="T">Expected type of resulting data.</typeparam>
		/// <param name="data">Data object to query.</param>
		/// <param name="uri">Path to resulting data.</param>
		/// <returns>Value at given URI.  Throws URI.InvalidURIPathException if path doesn't match data.</returns>
		public static T ReflectionQuery<T>(this IQueryableData data, URI uri)
		{
			try
			{
				return data.ReflectionFieldQuery<T>( uri );
			}
			catch( URI.InvalidURIPathException )
			{
			}

			return data.ReflectionPropertyQuery<T>( uri );
		}

		/// <summary>
		/// Uses System.Reflection to query for data. 
		/// </summary>
		/// <typeparam name="T">Expected type of resulting data.</typeparam>
		/// <param name="data">Data object to query.</param>
		/// <param name="uri">Path to resulting data.  Path should be defined using field names.</param>
		/// <returns>Value at given URI.  Throws URI.InvalidURIPathException if path doesn't match data.</returns>
		public static T ReflectionFieldQuery<T>(this IQueryableData data, URI uri)
		{
			string key = uri.TopDomain;

			if( data == null )
			{
				throw new URI.InvalidURIPathException( uri );
			}

			FieldInfo field = data.GetType().GetField( key );
			if( field == null )
			{
				throw new URI.InvalidURIPathException( uri );
			}

			URI deeper;
			if( uri.TryPop( out deeper ) )
			{
				IQueryableData queryable = field.GetValue( data ) as IQueryableData;
				if( queryable != null )
				{
					return queryable.ReflectionFieldQuery<T>( deeper );
				}

				throw new URI.InvalidURIPathException( uri );
			}

			return (T)field.GetValue( data );
		}

		/// <summary>
		/// Uses System.Reflection to query for data. 
		/// </summary>
		/// <typeparam name="T">Expected type of resulting data.</typeparam>
		/// <param name="data">Data object to query.</param>
		/// <param name="uri">Path to resulting data.  Path should be defined using property names.</param>
		/// <returns>Value at given URI.  Throws URI.InvalidURIPathException if path doesn't match data.</returns>
		public static T ReflectionPropertyQuery<T>(this IQueryableData data, URI uri)
		{
			string key = uri.TopDomain;

			if( data == null )
			{
				throw new URI.InvalidURIPathException( uri );
			}

			PropertyInfo property = data.GetType().GetProperty( key );
			if( property == null )
			{
				throw new URI.InvalidURIPathException( uri );
			}

			URI deeper;
			if( uri.TryPop( out deeper) )
			{
				IQueryableData queryable = property.GetValue( data ) as IQueryableData;
				if( queryable != null )
				{
					return queryable.ReflectionPropertyQuery<T>( deeper );
				}

				throw new URI.InvalidURIPathException( uri );
			}

			return (T)property.GetValue( data );
		}
	}
}
