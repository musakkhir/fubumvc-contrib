namespace FubuTask.Presentation.Services
{
    public interface IResponseStatusService
    {
        int Status { get; set; }
        string Description { get; set; }
    }
}