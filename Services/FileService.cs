using DSharpPlus.Entities;
using BaseBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBot.Services
{
    public class FileService
    {


        //deserialize config.json and return config object to work with
        public Config GetConfig()
        {
            var file = "./Config/Config.json";
            var data = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<Config>(data);
        }
    }
}
