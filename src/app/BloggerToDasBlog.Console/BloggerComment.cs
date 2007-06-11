using System;

namespace BloggerToDasBlog.Console {
	public class BloggerComment : IBloggerComment {
		public BloggerComment( ) {}

		public BloggerComment( string author, DateTime date, string body ) {
			_author = author;
			_date = date;
			_body = body;
		}

		public string Author {
			get { return _author; }
			set { _author = value; }
		}

		public DateTime Date {
			get { return _date; }
			set { _date = value; }
		}

		public string Body {
			get { return _body; }
			set { _body = value; }
		}

		private String _author;
		private DateTime _date;
		private String _body;
	}
}