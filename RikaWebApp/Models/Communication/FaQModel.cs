namespace RikaWebApp.Models.Communication
{
    public class FaQModel
    {
        public string QuestionTitle { get; set; } = null!;
        public string QuestionAnswer { get; set; } = null!;
        public string? QuestionUrl { get; set; }
    }
}
