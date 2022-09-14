namespace Store.DAL.Entities
{
    public class Product : BaseEntity
    {       
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Cost
        {
            get { return cost; }
            set
            {
                if (value > 0)
                    cost = value;
            }
        }
        private int cost;

        public Product(string name, string category, string desc, int cost)
        {          
            Name = name;
            Category = category;
            Description = desc;
            Cost = cost;
        }
    }
}

