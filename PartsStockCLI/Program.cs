namespace PartsStockCLI
{
    class Program
    {
        public static void Main()
        {
            Display.Art();

            Route r = new Route();
            r.Routed();
        }

        public class Display
        {
            public static void Art()
            {
                Console.Title = "Parts Stock CLI";
                Console.ForegroundColor = ConsoleColor.Cyan;
                string art =
                    "__________                __           ____     _________ __                 __    \n\\______   \\_____ ________/  |_  ______/  _ \\   /   _____//  |_  ____   ____ |  | __\n |     ___/\\__  \\\\_  __ \\   __\\/  ___/>  _ </\\ \\_____  \\\\   __\\/  _ \\_/ ___\\|  |/ /\n |    |     / __ \\|  | \\/|  |  \\___ \\/  <_\\ \\/ /        \\|  | (  <_> )  \\___|    < \n |____|    (____  /__|   |__| /____  >_____\\ \\/_______  /|__|  \\____/ \\___  >__|_ \\\n                \\/                 \\/       \\/        \\/                  \\/     \\/\n";
                Console.WriteLine(art);
                Console.ResetColor();
            }
        }

        public class Menu
        {
            public static int Home()
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("1) New Item");
                Console.WriteLine("2) Item Search");
                Console.WriteLine("3) Sourcing");
                Console.WriteLine("4) Storage Location Setup");
                Console.WriteLine("5) Exit");
                Console.WriteLine("------------------------");
                return Read.Int("Choice: ", min: 1);
            }
        }

        class Route
        {
            public string Pather(string fileName)
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    fileName);
            }

            public void Routed()
            {
                StorageLocations storageLocations = new StorageLocations();
                AddItem add = new AddItem();
                Sourcing sourcing = new Sourcing();
                ItemSearch search = new ItemSearch();

                bool running = true;
                while (running)
                {
                    Console.WriteLine();
                    int userIn = Menu.Home();
                    switch (userIn)
                    {
                        case 1:
                            add.NewItem();
                            break;
                        case 2:
                            search.Menu();
                            break;
                        case 3:
                            sourcing.Main();
                            break;
                        case 4:
                            storageLocations.StorageMain();
                            break;
                        case 5:
                            Console.Clear();
                            Console.WriteLine("Come back soon!");
                            running = false;
                            break;
                        case 6:
                            DeleteFile();
                            break;
                        case 7:
                            OverwriteFile();
                            break;
                        default:
                            Console.WriteLine("Invalid selection.");
                            break;
                    }
                }
            }

            private void DeleteFile()
            {
                string name = Read.RequiredText("File to be deleted: ");
                string path = Pather(name);

                if (!File.Exists(path))
                {
                    Console.WriteLine($"No such file: {path}");
                    return;
                }

                if (!Read.Yes($"Delete {path}? (y/N): "))
                {
                    Console.WriteLine("Cancelled.");
                    return;
                }

                File.Delete(path);
                Console.WriteLine("File deleted.");
            }

            private void OverwriteFile()
            {
                string name = Read.RequiredText("Filename to be overwritten: ");
                string path = Pather(name);

                if (!Read.Yes($"Overwrite {path} with an empty list? (y/N): "))
                {
                    Console.WriteLine("Cancelled.");
                    return;
                }

                File.WriteAllText(path, "[]");
                Console.WriteLine("File overwritten.");
            }
        }
    }
}
        

    



