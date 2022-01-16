using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoCoins.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoCoins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoCoinsController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public CryptoCoinsController(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        // GET: api/<CryptoCoins>
        [HttpGet]
        public IEnumerable<Coins> GetAll()
        {
            var allCoins = this._databaseContext.CryptoCoins.ToList();
            return allCoins;
        }

        // GET api/<CryptoCoins>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var coin = await this._databaseContext.CryptoCoins.FindAsync(id);
            if(coin == null)
            {
                return NotFound();
            }
            return Ok(coin);
        }

        // POST api/<CryptoCoins>
        [HttpPost]
        public async Task<bool> Post([FromBody] Coins request)
        {
            try
            {
                await this._databaseContext.CryptoCoins.AddAsync(request);
                var save = await this._databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // PUT api/<CryptoCoins>/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody] Coins request)
        {
            try
            {
                var coin = this._databaseContext.CryptoCoins.FirstOrDefault(c => c.CoinId == id);
                if (coin == null) return false;

                coin.CoinName = request.CoinName;
                coin.CoinValue = request.CoinValue;
                this._databaseContext.CryptoCoins.Update(coin);
                await this._databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        // DELETE api/<CryptoCoins>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            try
            {
                var coin = this._databaseContext.CryptoCoins.FirstOrDefault(c => c.CoinId == id);
                if (coin == null) return false;

                this._databaseContext.CryptoCoins.Remove(coin);
                this._databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //HealthCheck
        [HttpGet("hc")]
        public bool CheckHealth()
        {
            try
            {
                var coin = this._databaseContext.CryptoCoins.ToList();
                
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}
