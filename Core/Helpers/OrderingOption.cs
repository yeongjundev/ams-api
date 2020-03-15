namespace Core.Helpers
{
    public class OrderingOption
    {
        public string OrderBy { get; set; } = "CreateDateTime";

        public bool Desc { get; set; } = true;
    }
}