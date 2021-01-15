using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Collections._3.ConcurrentDictionay_AvoidRaceConditions
{
	public static class TShirtProvider
	{
		public static ImmutableArray<TShirt> AllShirts { get; } = ImmutableArray.Create(
			new TShirt("igeek", "IGeek", 500),
			new TShirt("bigdata", "Big Data", 600),
			new TShirt("ilovenode", "I Love Node", 75),
			new TShirt("kubernetes", "Kubernetes", 400),
			new TShirt("docker", "Docker", 350),
			new TShirt("qcon", "QCon", 300),
			new TShirt("react", "React", 60000),
			new TShirt("angular", "Angular", 60000)
		);

		public static ImmutableDictionary<string, TShirt> AllShirtsByCode { get; } = AllShirts.ToImmutableDictionary(x => x.Code); 

		public static TShirt SelectRandomShirt()
		{
			int selectedIndex = Rnd.NextInt(AllShirts.Length);
			return AllShirts[selectedIndex];
		}
	}
}
