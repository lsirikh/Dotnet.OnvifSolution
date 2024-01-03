using Dotnet.OnvifSolution.Factories;
using Dotnet.OnvifSolution.Models;

namespace Dotnet.OnvifSolution.UnitTest;

[TestClass]
public class UnitTestConnetion
{
    CameraDeviceModel CameraDeviceModel;


    [TestInitialize]
    public void Initialize()
    {
        CameraDeviceModel = new CameraDeviceModel() 
        {
            IpAddress = "192.168.1.118",
            UserName = "admin",
            Password = "sensorway1",
        };
    }

    [TestMethod]
    public async Task TestMethod1()
    {
        await Task.Delay(1000);
        await ServiceFactory.CreateDeviceClientAsync(CameraDeviceModel.IpAddress, CameraDeviceModel.UserName, CameraDeviceModel.Password);
        Assert.IsNotNull(CameraDeviceModel);

    }
}