using FireForecasting.DAL.Entityes.Incidents;
using FireForecasting.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FireForecasting.DAL.Entityes.Base
{
    public class Region : Entity
    {
        //public virtual ICollection<Fire> Fires { get; set; }
        
        public string Name { get; set; }

        //public int FiresCount
        //{
        //    get
        //    {
        //        return _FireRepository.Items.Where(x => x.Region == Name).ToList();
        //    }
        //    set
        //    {
        //        _FiresInRegion = value;
        //    }
        //}

        public IRepository<Fire> _FireRepository { get; }

        public Region(string name, IRepository<Fire> fireRepository)
        {
            Name = name;
            _FireRepository = fireRepository;
        }
        public Region()
        {

        }
    }
}
