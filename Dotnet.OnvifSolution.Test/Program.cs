using Dotnet.OnvifSolution.Models;
using Dotnet.OnvifSolution.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.OnvifSolution.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var cameraDeivceModel = new CameraDeviceModel() 
            {
                IpAddress = "192.168.1.119:80",
                UserName = "admin",
                Password = "sensorway1",
            };


            MainAsync(cameraDeivceModel).Wait();

            
        }

        static async Task MainAsync(CameraDeviceModel cameraDeivceModel)
        {
            var deviceService = new DeviceService();
            var cameraDevice =await deviceService.InitializeOnvif(cameraDeivceModel);
            var json = JsonConvert.SerializeObject(cameraDevice);
        }
    }
}
