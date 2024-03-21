using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMate.Models
{
    public class Card
    {
        public Card() {

        }
        public Card(int id, string content, float padVal) {
            Id = id;
            Content = content;
            PadVal = padVal;
        }

        public int Id { get; set; }


        public string Content { get; set; }

        public float PadVal { get; set; }
    }
}
