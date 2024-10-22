using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencesGame
{
    public class Connection
    {
        public Orientation Direction;
        public TileState Color;
        public int Row;
        public int Collumn;

        protected bool Equals(Connection other)
        {
            return Direction == other.Direction && Color == other.Color && Row == other.Row && Collumn == other.Collumn;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Direction;
                hashCode = (hashCode*397) ^ (int) Color;
                hashCode = (hashCode*397) ^ Row;
                hashCode = (hashCode*397) ^ Collumn;
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj.GetType() == this.GetType() && Equals((Connection) obj);
        }
    }
}
