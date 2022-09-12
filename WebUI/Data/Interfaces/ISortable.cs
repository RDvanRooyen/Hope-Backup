namespace WebUI.Data.Interfaces
{
    /// <summary>
    /// defines required fields to implement ISortable
    /// </summary>
    public interface ISortable
    {
        public float? SortOrder { get; set; }
    }
}
