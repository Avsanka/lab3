using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class NumOrder : INotifyPropertyChanged
    {
        private string order;
        public NumOrder(string order)
        {
            this.order = order;
        }
        public string Order_
        {
            get
            {
                return this.order;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    class Place
    {
        public string place { get; set; }
    }

    public class Transport
    {
        private string tran;
        public Transport(string tran)
        {
            this.tran = tran;
        }
        public string Tran
        {
            get
            {
                return this.tran;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    class Departure
    {
        public NumOrder NumOrder { get; set; }
        public Place Place { get; set; }
        public Transport Transport { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Заказ:\n" + NumOrder.Order_ + "\nТранспортом:\n");
            sb.Append(Place.place + "\nОтправлен клиенту в:\n" + Transport.Tran);
            return sb.ToString();
        }
    }
    abstract class DepartureBuilder
    {
        public Departure Departure { get; private set; }
        public void CreateDeparture()
        {
            Departure = new Departure();
        }
        public abstract void SetNumOrder(string order);
        public abstract void SetPlace(string place);
        public abstract void SetTransport(string tran);
    }
    class Departurer
    {
        public Departure Depart(DepartureBuilder departureBuilder, string order, string place, string tran)
        {
            departureBuilder.CreateDeparture();
            departureBuilder.SetNumOrder(order);
            departureBuilder.SetPlace(place);
            departureBuilder.SetTransport(tran);
            return departureBuilder.Departure;
        }
    }
    class ProductDepartureBuilder : DepartureBuilder
    {
        public override void SetNumOrder(string order)
        {
            this.Departure.NumOrder = new NumOrder(order);
        }

        public override void SetPlace(string place)
        {
            this.Departure.Place = new Place {place = place };
        }
        public override void SetTransport(string tran)
        {
            this.Departure.Transport = new Transport(tran);
        }
    }
}
