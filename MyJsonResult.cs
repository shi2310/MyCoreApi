namespace MyCoreApi
{
    public class MyJsonResult
    {
        public bool Success { get; set; } = false;
        public string Msg { get; set; }
        public object Data { get; set; }
    }
}
