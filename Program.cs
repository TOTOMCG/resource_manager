class Program
{
    private static string PATH = "C:/Users/ARTEM/RiderProjects/resource_manager"; // Путь для инициализации
    
    static void Main()
    {
        ResourceManager resourceManager = new ResourceManager(PATH);

        int id1 = resourceManager.Load("/assets/cool_text1.txt");
        int id2 = resourceManager.Load("/assets/cool_text2.txt");
        int id5 = resourceManager.Load("/assets/cool_text1.txt");
        int id3 = resourceManager.Load("fake");

        if (resourceManager.Has(id1))
        {
            var res = resourceManager.Get(id1);
            Console.WriteLine($"Получен ресурс: {res}");
            Console.WriteLine($"Размер ресурса {res.Length} байт");
        }

        resourceManager.Unload(id1);
        
        if (!resourceManager.Has(id1))
        {
            Console.WriteLine($"Ресурс с ID {id1} успешно выгружен.");
        }

        resourceManager.Reload(id1);
        resourceManager.Reload(id2);
        
        int id4 = resourceManager.Load("/cool_text1.txt");
        
        resourceManager.ReloadAll();
        resourceManager.UnloadAll();
    }
}