namespace LocatorTask.Utils;

public sealed class CsvReader : IDisposable
{
    private static string path = "C:\\Users\\Viktoriia_Sherstiuk\\Desktop\\ATM\\Locators\\Task\\locators\\LocatorTask\\Resources\\data.csv";
    private string[] currentData;
    private StreamReader reader;
    private static CsvReader csvreader;
    private static readonly object _Lock = new object();

    private CsvReader()
    {
        if (!File.Exists(path)) throw new InvalidOperationException("path does not exist");
        Initialize();
    }

    public static CsvReader GetReader
    {
        get
        {
            lock (_Lock)
                return csvreader ??= new CsvReader();
        }
    }

        private void Initialize()
    {
        FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        reader = new StreamReader(stream);
    }

    public bool Next()
    {
        string current = null;
        if ((current = reader.ReadLine()) == null) return false;
        currentData = current.Split(',');
        return true;
    }

    public string this[int index]
    {
        get { return currentData[index]; }
    }

    public void Dispose()
    {
        reader.Close();
    }
}