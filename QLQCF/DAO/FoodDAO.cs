﻿using QLQCF.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QLQCF.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            set => instance = value; }
        private FoodDAO() { }
        public List<Food> GetListFoodByCateID(int id)
        {
            List<Food> listFood = new List<Food>();
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from Food where Active = 1 and ID_FoodCategory = " + id);
            foreach (DataRow row in data.Rows)
            {
                Food food = new Food(row);
                listFood.Add(food);
            }
            return listFood;
        }
        public int GetFoodIdwithName(string name)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from Food where NameFood=N'" + name + "'");
            if(data.Rows.Count > 0)
            {
                return new Food(data.Rows[0]).Id;
            }
            return -1;
        }

        public float GetPricewithName(string name)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from Food where NameFood=N'" + name + "'");
            if (data.Rows.Count > 0)
            {
                Food food = new Food(data.Rows[0]);
                return food.Price;
            }
            return -1;
        }


        public Food GetFoodByBillInfo(BillInfo billInfo)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from Food where ID_Food='" + billInfo.IdFood + "' and active =1");
            if (data.Rows.Count > 0)
            {
                return new Food(data.Rows[0]);
            }
            return null;
        }

        //cua Dong
        public bool InsertFood(string name, int categoryid, float price)
        {
            string query = string.Format("Insert into dbo.Food(NameFood,ID_FoodCategory,Price) values (N'{0}',{1},{2})", name, categoryid, price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateFood(string name, int categoryid, float price, int id)
        {
            string query = string.Format("update dbo.Food set NameFood = N'{0}', ID_FoodCategory = {1}, Price = {2} where ID_Food = {3}", name, categoryid, price, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteFood(int id)
        {
            string query = string.Format("delete Food where ID_Food = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public DataTable GetListFood()
        {
            return DataProvider.Instance.ExecuteQuery("select * from Food");
        }
    }
}
