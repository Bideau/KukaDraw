using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace KukaDraw.Brain
{
    class Interpretor
    {
        private ArrayList data;

        public Interpretor(ArrayList data)
        {
            this.data = data;
        }

        public ArrayList getData()
        {
            return this.data;
        }

        public void setData(ArrayList data){
            this.data = data;
        }

        public void interpretation()
        {

        }



    }
}
