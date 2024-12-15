using Core.Entity;

namespace Core.DTO
{
    public class StorageDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public static implicit operator StorageDTO(Storage storage) => new()
        {
            Id = storage.Id,
            Name = storage.Name,
        };
    }
}



