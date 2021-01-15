using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Collections._2.Concurrent_Dictionary
{
    public class TShirtProvider
    {
		//ImmutableArray is unmodifiable and thread-safe
		public static ImmutableArray<TShirt> AllShirts { get; } = ImmutableArray.Create(//ImmutableArray can't be populated after instantiation.so create inline
			new TShirt("igeek", "IGeek", 500),
			new TShirt("bigdata", "Big Data", 600),
			new TShirt("ilovenode", "I Love Node", 750),
			new TShirt("kcdc", "kcdc", 400),
			new TShirt("docker", "Docker", 350),
			new TShirt("qcon", "QCon", 300),
			new TShirt("ml", "Machine Learning", 60000),
			new TShirt("ai", "Artificial Intelligence", 60000)
		);
	}
}
