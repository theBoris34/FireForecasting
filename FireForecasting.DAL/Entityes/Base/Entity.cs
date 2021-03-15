using FireForecasting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.DAL.Entityes.Base
{
    public abstract class Entity:IEntity
    {
        public int Id { get; set; }
    }
}
