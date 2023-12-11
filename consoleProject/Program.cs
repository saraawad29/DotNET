namespace consoleProject;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var myListe = new List<object> {1,2,3,4,5,6,7,8,9};
        var somme = Sum(myListe);
        Console.WriteLine($"Somme = {somme}");

    }
        public static int Sum(IEnumerable<object> valeurs)
        {
            var sum = 0;
            foreach (var item in valeurs)
            {
                switch (item)
                {
                    case 0:
                        break;
                    case int val:
                        sum += val;
                        break;
                    case IEnumerable<object> subList when subList.Any():
                        sum += Sum(subList);
                        break;
                    case IEnumerable<object> subList:
                        break;
                    case null:
                        break;
                    default:
                        throw new ArgumentException("invalid item type");
                }
            }
            return sum;
        }
}
