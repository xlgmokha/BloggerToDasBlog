using System;
using System.Collections.Generic;
using System.IO;

namespace BloggerToDasBlog.Console {
	internal class Program {
		private static void Main( ) {
			IList< IBloggerEntry > bloggerEntries =
				BloggerReader.Read( new Uri( Path.Combine( Environment.CurrentDirectory, "mokhan.blogspot.com.xml" ) ) );

			foreach ( IBloggerEntry bloggerEntry in bloggerEntries ) {
				new DasBlogWriter( ).Write( bloggerEntry );
			}
			System.Console.WriteLine( bloggerEntries );
			System.Console.WriteLine( bloggerEntries.Count + " Entries have been created!" );
			System.Console.ReadLine( );
		}
	}
}