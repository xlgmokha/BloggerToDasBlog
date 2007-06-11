using System;

namespace BloggerToDasBlog.Console {
	public interface IBloggerComment {
		string Author { get; set; }

		DateTime Date { get; set; }

		string Body { get; set; }
	}
}