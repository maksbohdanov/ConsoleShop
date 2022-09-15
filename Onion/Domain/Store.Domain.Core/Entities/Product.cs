namespace Store.Domain.Core.Entities
{
    /// <summary>
    /// Class Product used for creating products.  
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// The cost.
        /// </summary>
        private int cost;

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>The cost.</value>
        public int Cost
        {
            get { return cost; }
            set
            {
                if (value > 0)
                    cost = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <param name="desc">The description.</param>
        /// <param name="cost">The cost.</param>
        public Product(string name, string category, string desc, int cost)
        {          
            Name = name;
            Category = category;
            Description = desc;
            Cost = cost;
        }
    }
}

