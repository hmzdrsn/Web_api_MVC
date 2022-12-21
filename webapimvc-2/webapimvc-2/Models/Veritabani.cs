using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Security.Cryptography.X509Certificates;

namespace webapi_1.Models
{
    public class Veritabani
    {
        private static Dictionary<int, item> _liste;
        
        
        
        static Veritabani()
        {
            _liste = new Dictionary<int, item>();
            _liste.Add(1, new item
            {
                id=1,
                carName="Honda",
                description="Altın"
            });
            _liste.Add(2, new item
            {
                id = 2,
                carName = "Opel",
                description = "Beyaz"
            });
            _liste.Add(3, new item
            {
                id = 3,
                carName = "BMW",
                description = "Siyah"
            });
            _liste.Add(4, new item
            {
                id = 4,
                carName = "Mercedes",
                description = "Beyaz"
            });
            
            
            
        }
        public static void Add(int id, string carName, string description)
        {
            int key = id;
            item yedekModel = new item() 
            {
                id=id,carName=carName,description=description
            };

            if (_liste.ContainsKey(key))
            {
                return ;
            }
            else
            {
                _liste.Add(key,yedekModel);
            }
           
        }
        public static void Delete(int id)
        {
            int key = id;
            if(_liste.ContainsKey(key))
            {
                _liste.Remove(key);
            }
        }
        public static void Update(int id, string carName, string description)
        {
            int key = id;
            item yedekModel = new item()
            {
                id = id,
                carName = carName,
                description = description
            };
            if (_liste.ContainsKey(key))
            {
                _liste[key]=yedekModel;
            }

            

        }
        public static IEnumerable<item> Liste
        {
            get { return _liste.Values; }
        }
    }
}
