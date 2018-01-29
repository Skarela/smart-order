using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models.ViewModel
{
    public class ClosureViewModel
    {
        public List<Branch> ListBranch { get; set; }
        public List<Closure> ListClosure { get; set; }
        public Closure ClosureSelected { get; set; }

        public ClosureViewModel()
        {
            this.ListBranch = new List<Branch>();
            this.ListClosure = new List<Closure>();
        }

        public ClosureViewModel(List<Branch> listBranch, List<Closure> listClosure)
            : this(listBranch, listClosure, new Closure())
        {

        }

        public ClosureViewModel(List<Branch> listBranch, List<Closure> listClosure, Closure closureSelected)
        {
            this.ListBranch = listBranch;
            this.ListClosure = listClosure;
            this.ClosureSelected = closureSelected;
        }
    }
}