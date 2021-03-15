/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// 02/28/2021

using System.Text;

using SerializeField = UnityEngine.SerializeField;

namespace com.keg.mvvm.util
{
    /// <summary>
	/// Represents a path to data.
	/// The uri string is in the format "key.field.field.etc".
	/// </summary>
    [System.Serializable]
    public struct URI
    {
        private static string[] URIStringToDomainStack( string uri )
        {
            string[] domainStack;

            if( string.IsNullOrEmpty( uri ) )
            {
                domainStack = new string[ 0 ];
            }
            else
            {
                domainStack = uri.Split( '.' );
            }

            return domainStack;
        }

        private static string DomainStackToURIString( string[] domainStack )
		{
            StringBuilder uriStringBuilder = new StringBuilder();
            int domainCount = domainStack.Length;
            for( int i = 0; i < domainCount; ++i )
			{
                uriStringBuilder.Append( domainStack ).Append( '.' );
			}

            return uriStringBuilder.ToString();
		}

        [SerializeField] private string _uriString;
        public string URIString
		{
            get
			{
                if( string.IsNullOrEmpty( _uriString ) )
				{
                    _uriString = DomainStackToURIString( _domainStack );
				}

                return _uriString;
			}
		}

        private string[] _domainStack;

        public string TopDomain
		{
            get
			{
                if( _domainStack == null || _domainStack.Length == 0 )
				{
                    _domainStack = URIStringToDomainStack( _uriString );
				}
                if( _domainStack.Length == 0 )
				{
                    throw new EmptyURIException();
				}

                return _domainStack[ 0 ];
			}
		}

        public URI( string uri )
		{
            _uriString = uri;
            _domainStack = URIStringToDomainStack( _uriString );
		}

        public URI( params string[] domains )
		{
            _domainStack = domains;
            _uriString = DomainStackToURIString( _domainStack );
		}

        public bool TryPop( out URI uri )
		{
            uri = default;

            if( _domainStack == null || _domainStack.Length == 0 )
			{
                _domainStack = URIStringToDomainStack( _uriString );
			}

            int domainCount = _domainStack.Length;
            if( domainCount <= 1 )
			{
                return false;
			}

            string[] poppedDomain = new string[ domainCount - 1 ];
            for( int i = 1; i < domainCount; ++i )
			{
                poppedDomain[ i - 1 ] = _domainStack[ i ];
			}

            uri._domainStack = poppedDomain;
            uri._uriString = DomainStackToURIString( uri._domainStack );

            return true;
		}

		public override bool Equals( object obj )
		{
            if( string.IsNullOrEmpty( _uriString ) )
			{
                _uriString = DomainStackToURIString( _domainStack );
			}

			switch( obj )
			{
            case string stringObj:
                return _uriString == stringObj;
            case URI uriObj:
                return _uriString == uriObj.URIString;
            default:
                return false;
			}
		}

        public static implicit operator string( URI uri ) => uri.URIString;
        public static explicit operator URI( string path ) => new URI( path );

		public class EmptyURIException : System.Exception
		{
            public EmptyURIException() : base( "URI contains no domain stack or uri string" ) {}
		}

        public class InvalidURIPathException : System.Exception
		{
            public InvalidURIPathException(URI uri) : base( $"URI {uri.URIString} does not correspond to a valid data path") {}
		}
    }
}
