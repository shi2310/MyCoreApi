namespace MyCoreApi.Entity
{
    public class TodoItem
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] 取消自增
        public long Id { get; set; }

        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}