namespace Dotnet.OnvifSolution.Base.Models
{
    public interface IConnectionModel
    {
        string DeviceName { get; set; }
        string IpAddress { get; set; }
        string Password { get; set; }
        string UserName { get; set; }

    }
}