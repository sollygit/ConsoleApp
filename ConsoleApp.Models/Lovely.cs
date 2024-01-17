using System;

namespace ConsoleApp.Models
{
    public abstract class Spouse
    {
        const string empty = " ";
        const string dot = "*";
        readonly int[] writePattern;
        protected int currentLineNumber = 0;

        protected Spouse(string name, int[] writePattern)
        {
            this.writePattern = writePattern;
            Name = name;
        }

        public string Name { get; private set; }

        public bool Done { get { return currentLineNumber >= writePattern.Length; } }

        public virtual string GetNextLine()
        {
            string line = empty + empty;

            for (int i = 0; i < 32; i++)
                line = ((int)Math.Pow(2, i) & writePattern[currentLineNumber]) > 0 ? line + dot : line + empty;

            currentLineNumber++;

            return line;
        }
    }

    public class Husband : Spouse
    {
        public Husband(string name) : base(name, new int[] { 0x10000004, 0x8000008, 0x4000010, 0x2000020, 0x1000040, 0x800080, 0x400100, 0x200200, 0x100400, 0x80800, 0x41000, 0x22000, 0x14000, 0x8000 }) { }

        public override string GetNextLine()
        {
            string line = base.GetNextLine();

            if (currentLineNumber == 1)
                line = line.Substring(0, line.Length / 2 - Name.Length / 2) + Name + line.Substring(line.Length / 2 + Name.Length / 2 + Name.Length % 2);

            return line;
        }
    }

    public class Wife : Spouse
    {
        public Wife(string name) : base(name, new int[] { 0x0, 0x1F007C0, 0x2080820, 0x4041010, 0x8022008, 0x1001C004, 0x20000002, 0x20000002, 0x20000002, 0x20000002, 0x20000002, 0x20000002, 0x20000002 }) { }

        public override string GetNextLine()
        {
            string line = base.GetNextLine();

            if (currentLineNumber == 12)
                line = line.Substring(0, line.Length / 2 - Name.Length / 2) + Name + line.Substring(line.Length / 2 + Name.Length / 2 + Name.Length % 2);

            if (currentLineNumber == 13)
                line = line.Substring(0, line.Length / 2) + "&" + line.Substring(line.Length / 2 + 1);

            return line;
        }
    }

    public static class Lovely
    {
        static bool forever = true;

        private static void Love(Spouse spouse)
        {
            Console.WriteLine(spouse.GetNextLine());
            forever = !spouse.Done;
        }

        public static void Test()
        {
            Husband dotnet = new Husband(".NET");
            Wife solly = new Wife("Solly");

            do
            {
                Love(solly);
            } while (forever);

            do
            {
                Love(dotnet);
            } while (forever);
        }
    }
}
