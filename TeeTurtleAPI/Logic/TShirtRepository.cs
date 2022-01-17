using System;
using System.Collections.Generic;
using System.IO;
using Common;
using Newtonsoft.Json;

namespace TeeTurtleAPI.Logic
{
    public class TShirtRepository
    {
        public IEnumerable<TShirt> TShirts = Array.Empty<TShirt>();

        public TShirtRepository()
        {
            string data = File.ReadAllText("api.json");

            TShirts = JsonConvert.DeserializeObject<IEnumerable<TShirt>>(data);
        }
    }
}
