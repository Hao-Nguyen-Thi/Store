using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Mobie_store.Models.Entity;
using Mobie_store.Hellper;
using System.Data;
using System.Diagnostics;
using Mobie_store.Models.Function;
namespace Mobie_store.Models.Function
{
    public class orderFnc
    {
        public orderFnc()
        {
        }

        public int id { get; set; }

        public int? users_id { get; set; }

        public int total_money { get; set; }

        public DateTime? date_create { get; set; }

        public string method { get; set; }

        public List<order_detail> orderDetail { set; get; }


        public static List<orderFnc> Get()
        {
            using (var db = SetupConnection.ConnectionFactory())
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                return db.Query<orderFnc>("SELECT * FROM dbo.orders").ToList();
            }
        }

        public static string getUser(int? id)
        {
            using (var db = SetupConnection.ConnectionFactory())
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                return db.Query<user>("SELECT fullname FROM dbo.users WHERE id ='{0}'",id).ToString();
            }
        }

        public static orderFnc Get(int id)
        {
            orderFnc b = new orderFnc();
            b = Get().Find(x => x.id == id);
            string query = string.Format("SELECT * FROM dbo.order_detail WHERE order_id ='{0}'", id);
            using (var db = SetupConnection.ConnectionFactory())
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                b.orderDetail = db.Query<order_detail>(query).ToList();
                foreach (var item in b.orderDetail)
                {
                    //item.ProductName = Product.Get(item.ProductID).Name;
                    item.product.name = productFnc.Get(item.product_id).name;
                }
            }
            return b;
        }

        public bool CreateBill(List<CartDetail> lst)
        {
            int check = -1;
            using (var db = SetupConnection.ConnectionFactory())
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    using (var transaction = db.BeginTransaction())
                    {
                        check = db.Execute("CreateBill",
                            new
                            {
                                date_create = this.date_create,
                                users_id = this.users_id,
                                total_money = this.total_money,
                                method = this.method
                            },
                            commandType: CommandType.StoredProcedure,
                            transaction: transaction);
                        transaction.Commit();

                        // Create Detai Bill
                        int currentBill = db.QuerySingleOrDefault<int>("SELECT IDENT_CURRENT ('orders')");
                        if (!CreateBillDetail(lst, currentBill))
                        {
                            return false;
                        }

                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    return false;
                }
            }
            if (check >= 1) return true;
            else return false;

        }

        public bool CreateBillDetail(List<CartDetail> lst, int currentBill)
        {
            int check = -1;
            try
            {
                using (var db = SetupConnection.ConnectionFactory())
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    foreach (CartDetail sp in lst)
                    {
                        using (var transaction = db.BeginTransaction())
                        {
                            check = db.Execute("ins_BillDetail",
                                new
                                {
                                    order_id = currentBill,
                                    quantity = sp.Quantity,
                                    money = sp.Price,
                                    product_id = sp.ID,
                                },
                                commandType: CommandType.StoredProcedure,
                                transaction: transaction);
                            transaction.Commit();
                        }
                    }
                }
                if (check >= 1) return true;
                return false;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool Del(int id)
        {
            int check = -1;
            using (var db =SetupConnection.ConnectionFactory())
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    using (var transaction = db.BeginTransaction())
                    {
                        check = db.Execute("DelBill",
                            new
                            {
                                order_id = id
                            },
                            commandType: CommandType.StoredProcedure,
                            transaction: transaction);
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    return false;
                }
            }
            if (check >= 1) return true;
            else return false;
        }

        public bool sendEmail()
        {
            return true;
        }
    }
}