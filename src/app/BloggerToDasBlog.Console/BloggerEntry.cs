using System;
using System.Collections.Generic;

namespace BloggerToDasBlog.Console {
	public class BloggerEntry : IBloggerEntry {
		public BloggerEntry( ) {
			_comments = new List< IBloggerComment >( );
		}

		public BloggerEntry( Uri url, string title, string body, string author, DateTime date,
		                     IList< IBloggerComment > comments ) {
			_url = url;
			_title = title;
			_body = body;
			_author = author;
			_date = date;
			_comments = comments;
		}

		public Uri Url {
			get { return _url; }
		}

		public string Title {
			get { return _title; }
			set { _title = value; }
		}

		public string Body {
			get { return _body; }
			set { _body = value; }
		}

		public string Author {
			get { return _author; }
			set { _author = value; }
		}

		public DateTime Date {
			get { return _date; }
			set { _date = value; }
		}

		public IList< IBloggerComment > Comments {
			get { return _comments; }
		}

		private Uri _url;
		private String _title;
		private String _body;
		private String _author;
		private DateTime _date;
		private IList< IBloggerComment > _comments;
	}
}