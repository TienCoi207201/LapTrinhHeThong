using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuTang_App.Src.Test
{
    class ItemE
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Money { get; set; }
        public string Address { get; set; }

        public List<ItemE> CreateTestItems()
        {
            var resultsList = new List<ItemE>();
            for (int i = 0; i < 15; i++)
            {
                var a = new ItemE()
                {
                    Id = i,
                    Address = "Test Excel Address at " + i,
                    Money = 20000 + i * 10,
                    Name = "Pham Hong Sang " + i
                };
                resultsList.Add(a);
            }
            return resultsList;
        }
    }
}
