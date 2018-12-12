using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// Per risposte xml...
// https://docs.microsoft.com/it-it/aspnet/core/web-api/advanced/formatting?view=aspnetcore-2.2

// Per salvare file
//https://stackoverflow.com/questions/48062184/how-can-i-write-to-a-file-in-wwwroot-with-asp-net-core-2-0-webapi?rq=1

namespace PrimaCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        Studenti Studenti;

        public ValuesController()
        {
            Studenti = new Studenti("dati.csv");
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Studente>> Get()
        {
            return Studenti;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<IEnumerable<Studente>> Post([FromBody] Studente value)
        {
            Studenti.Aggiungi( value );
            return Studenti;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // PUT api/values
        [HttpPut]
        public ActionResult<IEnumerable<Studente>> Put([FromBody] Studente value)
        {
            Studenti.Aggiorna( value );
            return Studenti;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Studente>> Delete(int id)
        {
            Studenti.Cancella( id );
            return Studenti;
        }
    }
}
