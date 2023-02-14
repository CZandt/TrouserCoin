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
    class Program
    {
        static void Main(string[] args)
        {

            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();


            Blockchain trouserCoin = new Blockchain(2, 100);

            Console.WriteLine("Starting Miner up");
            trouserCoin.MinePendingTransactions(wallet1);

            decimal bal1 = trouserCoin.GetBalanceOfWallet(wallet1);

            Console.WriteLine("Balance of Wallet 1 is $" + bal1.ToString() + " TrouserCoins");

            Transaction tx1 = new Transaction(wallet1, wallet2, 10);

            tx1.SignTransaction(key1);

            trouserCoin.addPendingTransaction(tx1);

            Console.WriteLine("Starting Miner up");
            trouserCoin.MinePendingTransactions(wallet2);

            bal1 = trouserCoin.GetBalanceOfWallet(wallet1);
            decimal bal2 = trouserCoin.GetBalanceOfWallet(wallet2);

            Console.WriteLine("Balance of Wallet 1 is $" + bal1.ToString() + " TrouserCoins");
            Console.WriteLine("Balance of Wallet 2 is $" + bal2.ToString() + " TrouserCoins");
            string blockJSON = JsonConvert.SerializeObject(trouserCoin, Formatting.Indented);
            Console.WriteLine(blockJSON);

            

            if (trouserCoin.IsChainValid())
            {
                Console.WriteLine("trouserCoin is Valid");
            }
            else
            {
                Console.WriteLine("trouserCoin is Invalid -- NOT BASED");
            }
              
            Console.ReadLine(); // Stops terminal so I can read it
        }
    }
}
