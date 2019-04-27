using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mobie_store.Models.Function
{
    public class Cart
    {
        private List<CartDetail> listHangHoa = new List<CartDetail>();

        public Cart()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<CartDetail> getCartDetailList()
        {
            return listHangHoa;
        }

        public void add(CartDetail tmp)
        {
            bool co = false;
            foreach (CartDetail i in listHangHoa)
                if (i.ID == tmp.ID) //Already exists
                {
                    i.Quantity += tmp.Quantity;
                    co = true;
                    break;
                }
            if (!co)
                listHangHoa.Add(tmp);
        }

        public void update(CartDetail tmp)
        {
            foreach (CartDetail i in listHangHoa)
                if (i.ID == tmp.ID)
                {
                    i.Quantity = tmp.Quantity;
                    if (tmp.Quantity == 0)
                        listHangHoa.Remove(tmp);
                    return;
                }
        }

        public void delete(int id)
        {
            foreach (CartDetail i in listHangHoa.ToList())
            {
                if (i.ID == id)
                    listHangHoa.Remove(i);
            }
        }

        public int getCount()
        {
            int sll = 0;
            foreach (CartDetail i in listHangHoa)
                sll += i.Quantity;
            return sll;
        }

        // Error string to int
        public int getTotalPrice()
        {
            int tong = 0;
            foreach (CartDetail i in listHangHoa)
                tong += i.Quantity * (i.Price);
            return tong;
        }
    }
}