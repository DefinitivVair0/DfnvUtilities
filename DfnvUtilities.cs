namespace DfnvUtilities
{
    public class Printer
    {
        public static DfnvUtilities.PrinterSeperator SegmentSeperator { get; } = new('+', "segm", true);
        public static DfnvUtilities.PrinterSeperator BlockSeparator { get; } = new('-', "blck");
        public static DfnvUtilities.PrinterSeperator LineSeperator { get; } = new(' ', "line");


        private List<DfnvUtilities.PrinterSeperator> seperators = [DfnvUtilities.Printer.SegmentSeperator, DfnvUtilities.Printer.BlockSeparator, DfnvUtilities.Printer.LineSeperator];
        private List<string> table;

        public int Chunksize { get; set; } = 64;
        public int Padding { get; set; } = 1;


        public Printer() => table = [];
        public Printer(IEnumerable<string> list) => table = [.. list];


        public string GetEntry(int segment) => table[segment];
        public string[] GetContents() => [.. table];

        public void Add(string value) => table.Add(value);
        public void Insert(int position, string value) => table.Insert(position, value);
        public void Remove(int position) => table.RemoveAt(position);
        public void Clear() => table.Clear();
        public void AddSeperator(DfnvUtilities.PrinterSeperator ps)
        {
            if (!seperators.Contains(ps)) seperators.Add(ps);
            table.Add(ps.Identifier);
        }
        public void Print()
        {
            Format();
            
            table.ForEach(item => Console.WriteLine(item));
        }

        private void Format()
        {
            int k = 0;
            while (k < table.Count)
            {
                if (table[k].StartsWith("$#")) 
                {
                    DfnvUtilities.PrinterSeperator seperator = seperators.Where(i => i.Identifier == table[k]).First();
                    table[k] = seperator.Build(Chunksize, Padding); k++; 
                }
                else
                {
                    if (table[k].Length > Chunksize)
                    {
                        IEnumerable<string> split = DfnvUtilities.Helpers.Split(table[k], Chunksize);
                        table.RemoveAt(k);
                        table.InsertRange(k, split);


                        for (int i = 0; i < split.Count(); i++)
                        {
                            table[k + i] = DfnvUtilities.Helpers.Pad(table[k + i], Padding, Chunksize);
                        }

                        k += split.Count();
                    }
                    else
                    {
                        table[k] = DfnvUtilities.Helpers.Pad(table[k], Padding, Chunksize);
                        k++;
                    }
                }
            }


            table.Insert(0, DfnvUtilities.Printer.SegmentSeperator.Build(Chunksize, Padding));
            table.Add(DfnvUtilities.Printer.SegmentSeperator.Build(Chunksize, Padding));
        }
    }
}
