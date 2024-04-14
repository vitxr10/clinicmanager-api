namespace ClinicManager.Application.Services
{
    public interface IPdfService
    {
        byte[] CreatePdf(string header, string content, string footer);
    }
}