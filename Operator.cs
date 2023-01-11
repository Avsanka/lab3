using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Product : INotifyPropertyChanged
    {
        private string type;
        public Product(string type)
        {
            this.type = type;
        }
        public string Type
        {
            get
            {
                return this.type;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    class Weight
    {
        public int Kilos { get; set; }
    }

    class Order
    {
        public Product Product { get; set; }
        public Weight Weight { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Заказ:\n" + Product.Type + "\n");
            sb.Append(Weight.Kilos + "(кг)\n создан, ожидайте ответа оператора\n");
            return sb.ToString();
        }
    }


    abstract class OrderBuilder
    {
        public Order Order { get; private set; }
        public void CreateOrder()
        {
            Order = new Order();
        }
        public abstract void SetProduct(string type);
        public abstract void SetWeight(int gramms);
    }
    class Operator
    {
        public Order Operate(OrderBuilder orderBuilder, int kilos, string type)
        {
            orderBuilder.CreateOrder();
            orderBuilder.SetProduct(type);
            orderBuilder.SetWeight(kilos);
            return orderBuilder.Order;
        }
    }
    class ProductOrderBuilder : OrderBuilder
    {
        public override void SetProduct(string type)
        {
            this.Order.Product = new Product(type);
        }

        public override void SetWeight(int kilos)
        {
            this.Order.Weight = new Weight { Kilos = kilos };
        }
    }

}