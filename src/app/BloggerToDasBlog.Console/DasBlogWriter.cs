using System;
using newtelligence.DasBlog.Runtime;

namespace BloggerToDasBlog.Console {
	public class DasBlogWriter {
		#region Constructors

		public DasBlogWriter( )
			: this( BlogDataServiceFactory.GetService( AppDomain.CurrentDomain.BaseDirectory, null ), "Journal" ) {}

		public DasBlogWriter( IBlogDataService service, String category ) {
			_service = service;
			_category = category;
		}

		#endregion

		#region Public Methods

		public void Write( IBloggerEntry bloggerEntry ) {
			Entry entry = new Entry( );
			entry.CreatedLocalTime = bloggerEntry.Date;
			entry.ModifiedLocalTime = bloggerEntry.Date;
			entry.Title =
				( bloggerEntry.Title.Length > 0
				  	? bloggerEntry.Title
				  	: bloggerEntry.Body.Substring( 0, Math.Min( 20, bloggerEntry.Body.Length ) ) );
			entry.Content = bloggerEntry.Body.Replace( Environment.NewLine, "<br />" );
			entry.EntryId = Guid.NewGuid( ).ToString( );
			entry.Categories = _category;
			entry.Author = bloggerEntry.Author;
			_service.SaveEntry( entry );
			if ( bloggerEntry.Comments.Count > 0 ) {
				foreach ( IBloggerComment bloggerComment in bloggerEntry.Comments ) {
					WriteComments( bloggerComment, entry.EntryId );
				}
			}
		}

		#endregion

		#region Private Methods

		private void WriteComments( IBloggerComment bloggerComment, String targetEntryId ) {
			Comment comment = new Comment( );
			comment.CreatedLocalTime = bloggerComment.Date;
			comment.ModifiedLocalTime = bloggerComment.Date;
			comment.TargetEntryId = targetEntryId;
			comment.Author = bloggerComment.Author;
			comment.Content = bloggerComment.Body;
			_service.AddComment( comment );
		}

		#endregion

		#region Private Fields

		private readonly IBlogDataService _service;
		private readonly string _category;

		#endregion
	}
}