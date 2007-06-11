using System;
using System.Collections.Generic;

namespace BloggerToDasBlog.Console {
	public interface IBloggerEntry {
		Uri Url { get; }

		string Title { get; set; }

		string Body { get; set; }

		string Author { get; set; }

		DateTime Date { get; set; }

		IList< IBloggerComment > Comments { get; }
	}
}