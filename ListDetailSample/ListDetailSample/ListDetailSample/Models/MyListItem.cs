namespace ListDetailSample.Models
{
    public class MyListItem
    {
        public string Title { get; }
        public string Detail { get; }

        public MyListItem(int num)
        {
            Title = $"Titel {num}";
            Detail = $"Das ist das Detail mit der Nummer {num}.";
        }
    }
}
