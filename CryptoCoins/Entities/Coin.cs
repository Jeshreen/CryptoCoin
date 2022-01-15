using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoCoins.Entities
{
    public class Coins
    {
        [Key]
        public int CoinId { get; set; }
        public string CoinName { get; set; }
        public double CoinValue { get; set; }
    }
}
