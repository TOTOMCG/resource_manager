public class ResourceManager(string path)
{
    private Dictionary<int, (byte[], string)> _resources = new(); // Храним два значения по одному ключу
    private int _nextId = 1;
    private string _initPath = path;
    
    /// <summary>
    /// Загружает файл с указанного пути в память. Возвращает уникальный ID.
    /// </summary>
    public int Load(string path)
    {
        string completePath = _initPath + path;
        if (!File.Exists(completePath))
        {
            Console.WriteLine($"Файл не найден: {completePath}");
            return -1;
        }

        byte[] fileData = File.ReadAllBytes(completePath); // Реальная загрузка файла в память

        int id = _nextId++;
        _resources[id] = (fileData, completePath);

        Console.WriteLine($"Загружен ресурс: {completePath} с ID: {id}");
        return id;
    }

    /// <summary>
    /// Получает содержимое ресурса по ID.
    /// </summary>
    public byte[] Get(int id)
    {
        if (_resources.ContainsKey(id))
            return _resources[id].Item1;

        throw new Exception($"Ресурс с ID {id} не найден.");
    }

    /// <summary>
    /// Проверяет наличие ресурса по ID.
    /// </summary>
    public bool Has(int id)
    {
        return _resources.ContainsKey(id);
    }

    /// <summary>
    /// Выгружает ресурс по ID.
    /// </summary>
    public void Unload(int id)
    {
        if (_resources.ContainsKey(id))
        {
            _resources.Remove(id);
        }
        else
        {
            Console.WriteLine($"Ресурс с ID: {id} не найден.");
        }
    }
    
    /// <summary>
    /// Перезагружает ресурс по ID по тому же пути 
    /// </summary>
    public int Reload(int id)
    {
        if (!_resources.ContainsKey(id))
        {
            Console.WriteLine($"Ресурс с ID: {id} не найден.");
            return -1;
        }

        string path = _resources[id].Item2;

        if (!File.Exists(path))
        {
            Console.WriteLine($"Файл не найден: {path}");
            return -1;
        }
        
        byte[] fileData = File.ReadAllBytes(path);
        _resources[id] = (fileData, path);
        Console.WriteLine($"Ресурс с ID {id} успешно перезагружен.");
        return 1;
    }

    /// <summary>
    /// Перезагружает все загруженные ресурсы
    /// </summary>
    public void ReloadAll()
    {
        foreach (KeyValuePair<int, (byte[], string)> resource in _resources)
        {
            Reload(resource.Key);
        }
        Console.WriteLine("Все ресурсы успешно перезагружены.");
    }

    /// <summary>
    /// Выгружает все загруженные ресурсы
    /// </summary>
    public void UnloadAll()
    {
        _resources.Clear();
        Console.WriteLine("Все ресурсы успешно выгружены.");
        
        GC.Collect(); // Должно освободить память
    }
}