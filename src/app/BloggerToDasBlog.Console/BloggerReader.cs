using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace BloggerToDasBlog.Console {
	public sealed class BloggerReader {
		#region Constructors

		private BloggerReader( ) {}

		#endregion

		#region Public Methods

		public static IList< IBloggerEntry > Read( Uri filePath ) {
			XmlDocument document = new XmlDocument( );
			document.LoadXml( File.ReadAllText( filePath.LocalPath ) );

			XmlNodeReader reader = new XmlNodeReader( document );
			reader.MoveToContent( );

			IList< IBloggerEntry > results = new List< IBloggerEntry >( );
			if ( reader.Read( ) ) {
				if ( reader.NodeType == XmlNodeType.Element ) {
					if ( String.Compare( reader.LocalName, BloggerEntryXmlElement.Root, true, CultureInfo.InvariantCulture ) == 0 ) {
						String entryNode;
						while ( !String.IsNullOrEmpty( entryNode = reader.ReadOuterXml( ) ) ) {
							IBloggerEntry entry = ParseSingle( entryNode );
							if ( null != entry ) {
								results.Add( entry );
							}
						}
						return results;
					}
				}
			}
			return null;
		}

		#endregion

		#region Private Methods

		private static IBloggerEntry ParseSingle( String resultXmlRecord ) {
			if ( String.IsNullOrEmpty( resultXmlRecord ) ) {
				return null;
			}
			XmlDocument document = new XmlDocument( );
			document.LoadXml( resultXmlRecord );
			XmlNodeReader reader = new XmlNodeReader( document );

			IBloggerEntry entry = new BloggerEntry( );
			reader.MoveToContent( );
			try {
				while ( reader.Read( ) ) {
					if ( reader.NodeType == XmlNodeType.Element ) {
						switch ( reader.LocalName.ToLower( CultureInfo.InvariantCulture ) ) {
							case BloggerEntryXmlElement.Title:
								entry.Title = reader.ReadString( );
								break;
							case BloggerEntryXmlElement.Body:
								entry.Body = reader.ReadString( );
								break;
							case BloggerEntryXmlElement.Author:
								entry.Author = reader.ReadString( );
								break;
							case BloggerEntryXmlElement.Date:
								DateTime dateTime;
								entry.Date = DateTime.TryParse( reader.ReadString( ), out dateTime ) ? dateTime : DateTime.MinValue;
								break;
							case BloggerCommentXmlElement.Root:
								String commentNode = reader.ReadOuterXml( );
								if ( !String.IsNullOrEmpty( commentNode ) ) {
									entry.Comments.Add( ParseComment( commentNode ) );
								}
								break;
						}
					}
				}
				return entry;
			}
			catch ( FormatException ) {
				return null;
			}
		}

		private static IBloggerComment ParseComment( string xml ) {
			if ( String.IsNullOrEmpty( xml ) ) {
				return null;
			}

			IBloggerComment comment = new BloggerComment( );
			XmlDocument document = new XmlDocument( );
			document.LoadXml( xml );
			XmlNodeReader reader = new XmlNodeReader( document );
			reader.MoveToContent( );

			while ( reader.Read( ) ) {
				if ( reader.NodeType == XmlNodeType.Element ) {
					switch ( reader.LocalName ) {
						case BloggerCommentXmlElement.Author:
							comment.Author = reader.ReadString( );
							break;
						case BloggerCommentXmlElement.Date:
							DateTime dateTime;
							comment.Date = DateTime.TryParse( reader.ReadString( ), out dateTime ) ? dateTime : DateTime.MinValue;
							break;

						case BloggerCommentXmlElement.Body:
							comment.Body = reader.ReadString( );
							break;
					}
				}
			}
			return comment;
		}

		#endregion
	}
}