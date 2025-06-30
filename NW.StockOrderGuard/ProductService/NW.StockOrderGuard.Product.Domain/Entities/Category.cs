using NW.StockOrderGuard.SharedKernel;

namespace NW.StockOrderGuard.Product.Domain.Entities
{
    public class Category : IEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }

        public Category(int id, string name, string description, bool isActive = true)
        {
            Id = id;
            Name = name;
            Description = description;
            IsActive = isActive;
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public void ChangeDescription(string newDescription)
        {
            Description = newDescription;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public override bool Equals(object obj)
        {
            if (obj is not Category other) return false;
            return Id == other.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
} 