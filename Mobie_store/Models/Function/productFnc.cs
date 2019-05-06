using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Mobie_store.Models.Entity;
using Mobie_store.Hellper;
using System.Data;
using System.Diagnostics;

namespace Mobie_store.Models.Function
{
    public partial class productFnc
    {
        public productFnc()
        {
        }
        public static List<product> Get()
        {
            using (var db = SetupConnection.ConnectionFactory())
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                return db.Query<product>("SELECT * FROM dbo.products").ToList();
            }
        }

        public static product Get(int ID)
        {
            return Get().Find(x => x.id == ID);
        }

        public static bool Put(product p)
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
                        check = db.Execute("Update_product",
                            new
                            {
                                id = p.id,
                                sku = p.sku,
                                name = p.name,
                                price = p.price,
                                Ghz = p.Ghz,
                                color = p.color,
                                sensor = p.sensor,
                                cpu = p.cpu,
                                ram = p.ram,
                                storage = p.storage,
                                camera_front = p.camera_front,
                                camera_rear = p.camera_rear,
                                battery = p.battery,
                                display = p.display,
                                sim = p.sim,
                                status = p.status
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

        public static bool Post(product p)
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
                        check = db.Execute("Ins_product",
                            new
                            {
                                id = p.id,
                                sku = p.sku,
                                name = p.name,
                                price = p.price,
                                Ghz = p.Ghz,
                                color = p.color,
                                sensor = p.sensor,
                                cpu = p.cpu,
                                ram = p.ram,
                                storage = p.storage,
                                camera_front = p.camera_front,
                                camera_rear = p.camera_rear,
                                battery = p.battery,
                                display = p.display,
                                sim = p.sim,
                                status = p.status

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

        public bool Delete()
        {
            using (var db = SetupConnection.ConnectionFactory())
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                return db.QuerySingleOrDefault<bool>("SELECT * FROM dbo.products");
            }
        }

        public bool Update_discount(product p)
        {
            int check = -1;
            using ( var db = SetupConnection.ConnectionFactory())
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    check = db.Execute("Update_product_discount",
                        new
                        {
                            id = p.id,
                            discount_id = p.discount_id
                        },
                        commandType: CommandType.StoredProcedure,
                        transaction: transaction);
                    transaction.Commit();
                }
            }
            if (check >= 1) return true;
            else return false;
        }
    }
}