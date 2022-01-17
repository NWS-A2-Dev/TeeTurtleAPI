using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using TeeTurtleAPI.Logic;

namespace TeeTurtleAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class TShirtController : ControllerBase
    {
        private TShirtRepository Repositrory;

        public TShirtController(TShirtRepository repo)
        {
            Repositrory = repo;
        }

        [HttpGet]
        public IEnumerable<TShirt> Get()
        {
            return (Repositrory.TShirts
                .Take(10));
        }

        [HttpGet("{page}")]
        public IEnumerable<TShirt> Get(int page)
        {
            try
            {
                return (Repositrory.TShirts
                    .Skip(10 * (page - 1))
                    .Take(10));
            }
            catch (Exception)
            {
                return (Array.Empty<TShirt>());
            }
        }
    }
}
