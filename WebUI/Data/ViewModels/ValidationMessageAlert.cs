namespace WebUI.Data.ViewModels
{
    public class ValidationMessageAlert
    {
        public int Step { get; set; }
        public string Message { get; set; }
        public bool Valid { get; internal set; }
    }
}
