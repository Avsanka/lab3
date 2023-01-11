using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace lab3
{
    public class AWMbegin : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string path = "C:\\Users\\Пользователь\\Desktop\\123.txt";

        private Product selectedProduct;

        private ObservableCollection<Product> product;

        private NumOrder selectedOrder;

        private ObservableCollection<NumOrder> numorder;

        private Transport selectedTransport;

        private ObservableCollection<Transport> transport;

        public AWMbegin()
        {
            transport = new ObservableCollection<Transport>()
            {
                new Transport("Автомобиль"),
                new Transport("Ж/Д"),
                new Transport("Самолёт")
            };
            numorder = new ObservableCollection<NumOrder>();
            product = new ObservableCollection<Product>()
            {
                new Product("Кирпич красный"),
                new Product("Кирпич серый"),
                new Product("Цемент")
            };
        }
        public Product SelectedProduct
        {
            get
            {
                return (this.selectedProduct);
            }
            set
            {
                this.selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        public ObservableCollection<Product> Products
        {
            get
            {
                return this.product;
            }
        }

        public NumOrder SelectedOrder
        {
            get
            {
                return (this.selectedOrder);
            }
            set
            {
                this.selectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }

        public ObservableCollection<NumOrder> NumOrders
        {
            get
            {
                return this.numorder;
            }
        }
        public Transport SelectedTransport
        {
            get
            {
                return (this.selectedTransport);
            }
            set
            {
                this.selectedTransport = value;
                OnPropertyChanged("SelectedTransport");
            }
        }

        public ObservableCollection<Transport> Transports

        {
            get
            {
                return this.transport;
            }
        }

        private int Price(int price, string city, string transport)
        {
            switch (city)
            {
                case "Томск":
                    {
                        switch (transport)
                        {
                            case "Автомобиль":
                                {
                                    price = price * 4 + 2500;
                                    break;
                                }
                            case "Ж/Д":
                                {
                                    price = price * 4 + 5000;
                                    break;
                                }
                            case "Самолёт":
                                {
                                    price = price * 4 + 10000;
                                    break;
                                }
                        }
                        break;
                    }
                case "Новосибирск":
                    {
                        switch (transport)
                        {
                            case "Автомобиль":
                                {
                                    price = price * 3 + 2500;
                                    break;
                                }
                            case "Ж/Д":
                                {
                                    price = price * 3 + 5000;
                                    break;
                                }
                            case "Самолёт":
                                {
                                    price = price * 3 + 10000;
                                    break;
                                }
                        }
                        break;
                    }
                case "Кемерово":
                    {
                        switch (transport)
                        {
                            case "Автомобиль":
                                {
                                    price = price * 2 + 2500;
                                    break;
                                }
                            case "Ж/Д":
                                {
                                    price = price * 2 + 5000;
                                    break;
                                }
                            case "Самолёт":
                                {
                                    price = price * 2 + 10000;
                                    break;
                                }
                        }
                        break;
                    }
            }
            return price;
        }
        
        private void WriteFile(string path, string text)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLineAsync(text);
            }
        }

        private Command makeOrder;

        public Command MakeOrder
        {
            get
            { 
                return makeOrder ?? (makeOrder = new Command(obj =>
                {
                    if (selectedProduct != null)
                    {
                        Operator operator_ = new Operator();
                        OrderBuilder builder = new ProductOrderBuilder();
                        Order order = operator_.Operate(builder, int.Parse(obj.ToString()), SelectedProduct.Type);
                        MessageBox.Show(order.ToString());
                        string str = SelectedProduct.Type + " --- " + obj.ToString();
                        numorder.Add(new NumOrder(str));
                    }
                    else MessageBox.Show("Заказ некорректен!");
                }));
            }
        }

        string priceStr;
        int price;
        private Command sendOrder;
        public Command SendOrder
        {
            get
            {
                return sendOrder ?? (sendOrder = new Command(obj =>
                {
                    if (selectedOrder != null && selectedTransport != null)
                    {
                        if ((SelectedOrder.Order_.Contains("Кирпич красный") && SelectedTransport.Tran.Contains("Самолёт"))
                        || (SelectedOrder.Order_.Contains("Кирпич серый") && SelectedTransport.Tran.Contains("Самолёт"))
                        || (SelectedOrder.Order_.Contains("Цемент") && SelectedTransport.Tran.Contains("Ж/Д")))
                            MessageBox.Show("Отправка невозможна, попробуйте выбрать другой заказ или транспорт");
                        else
                        {
                            Departurer departurer_ = new Departurer();
                            DepartureBuilder builder = new ProductDepartureBuilder();
                            Departure departure = departurer_.Depart(builder, SelectedOrder.Order_, SelectedTransport.Tran, obj.ToString());
                            priceStr = SelectedOrder.Order_;
                            priceStr = priceStr.Substring(priceStr.Length - 3);
                            price = int.Parse(priceStr);
                            price = Price(price, obj.ToString(), SelectedTransport.Tran);
                            numorder.Remove(SelectedOrder);
                            MessageBox.Show(departure.ToString() + "\nЦена: " + price + "рублей");
                            WriteFile(path, departure.ToString() + "\nЦена: " + price + "рублей\n");
                        }
                        
                    }
                    else MessageBox.Show("Сначала нужно выбрать заказ и транспорт!");
                }));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
