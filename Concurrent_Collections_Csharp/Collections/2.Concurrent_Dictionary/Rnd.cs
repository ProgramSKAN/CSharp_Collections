using System;
using System.Collections.Generic;
using System.Text;

namespace Collections._2.Concurrent_Dictionary
{
    public class Rnd
    {
        private static Random _generator = new Random();//new Random() is expensive to instantiate.so created static instance that entire app can use
        public static int NextInt(int max) => _generator.Next(max);//random value between 0 & max-1
        public static bool TrueWithProb(double probOfTrue) => _generator.NextDouble() < probOfTrue;//NextDouble > give random between 0.0 & 1.0
    }
}
