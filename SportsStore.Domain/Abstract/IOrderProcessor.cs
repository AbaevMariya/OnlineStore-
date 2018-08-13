using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Abstract
{
    public interface IOrderProcessor //Наша реализация IOrderProcessor будет обрабатывать заказы, отправляя их по электронной почте администратору сайта
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
