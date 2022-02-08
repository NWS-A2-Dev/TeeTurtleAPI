using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Microsoft.AspNetCore.Cors;
using TeeTurtleAPI.Logic;

namespace TeeTurtleAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class TShirtController : ControllerBase
    {
        private TShirtRepository Repository;

        public TShirtController(TShirtRepository repo)
        {
            Repository = repo;
        }

        [HttpGet]
        public IEnumerable<TShirt> Get()
        {
            return (Repository.TShirts
                .Take(24));
        }

        [HttpGet("page/{page}")]
        public IEnumerable<TShirt> Get(int page)
        {
            try
            {
                return (Repository.TShirts
                    .Skip(24 * (page - 1))
                    .Take(24));
            }
            catch (Exception)
            {
                return (Array.Empty<TShirt>());
            }
        }

        [HttpGet("{id}")]
        public TShirt Get(Guid id)
        {
            return (Repository.TShirts
                .FirstOrDefault(i => i.Id == id));
        }
    }
}
