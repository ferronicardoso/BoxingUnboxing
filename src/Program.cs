using System;
using System.Diagnostics;

namespace BoxingUnboxing
{
    class Program
    {
        static void Main(string[] args)
        {
             var f1 = new Foo {Value = 5};
            var f2 = new Foo {Value = 5};
            var f3 = new Foo {Value = 6};

            var sw = new Stopwatch();
            sw.Start();
            var gc0 = GC.CollectionCount(0);
            var gc1 = GC.CollectionCount(1);

            for (int i = 0; i < 10_000_000; i++)
            {
                if (!f1.Equals(f2))
                {
                   Console.WriteLine("Failed");
                }

                if (f1.Equals(f3))
                {
                    Console.WriteLine("Failed");
                }
            }

            Console.WriteLine($"Ellapsed time: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"GC0 count    : {GC.CollectionCount(0) - gc0 }");
            //Console.ReadLine();
        }
    }
    
    struct Foo
    {
        public int Value { get; set; }

        public override int GetHashCode()
        {
            return Value.GetHashCode() + this.GetHashCode();
        }        

        public bool Equals(Foo obj)
        {
            return Value == obj.Value;
        }
    }
}
