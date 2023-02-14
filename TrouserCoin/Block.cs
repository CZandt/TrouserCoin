using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Newtonsoft.Json;
using EllipticCurve;

namespace TrouserCoin
{
    class Block
    {
        public int Index { get; set; }
        public string previousHash { get; set; }
        public string timestamp { get; set; }
        public string Hash { get; set; }
        public int Nonce { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Block(int Index, string timestamp, List<Transaction> transactions, string previousHash = "")
        {
            this.Index = Index;
            this.timestamp = timestamp;
            this.Transactions = transactions;
            this.previousHash = previousHash;
            this.Hash = CalculateHash();
            this.Nonce = 0;
        }

        public string CalculateHash()
        {
            string blockData = this.Index + this.previousHash + this.timestamp + this.Transactions.ToString() + this.Nonce; // NOCNE?
            byte[] blockBytes = Encoding.ASCII.GetBytes(blockData); // gets byte array representation of the string
            byte[] hashByte = SHA256.Create().ComputeHash(blockBytes); //Computes the hash from the byte array

            return BitConverter.ToString(hashByte).Replace("-", "");
        }

        public void Mine(int difficulty)
        {
            while (this.Hash.Substring(0, difficulty) != new string('0', difficulty))
            {  
                this.Nonce++;
                this.Hash = this.CalculateHash();
                // Console.WriteLine("Mining: " + this.Hash);
            }

            Console.WriteLine("The block has been mined " + this.Hash);
        }

    }
}
