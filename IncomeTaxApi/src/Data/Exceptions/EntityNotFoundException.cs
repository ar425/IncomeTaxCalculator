namespace IncomeTaxApi.Data.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(in object entityId, Type entityType)
            : this(entityId.ToString() ?? string.Empty, entityType)
        {
        }

        private EntityNotFoundException(string entityId, Type entityType) : base(FormatMessage(entityId, entityType))
        {
            Id = entityId;
            EntityType = entityType;
        }

        /// <summary>
        /// The Id of the entity that was not found.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The name of the type of entity that was not found.
        /// </summary>
        public Type EntityType { get; }

        private static string FormatMessage(string entityId, Type entityType)
        {
            return $"{entityType.Name} '{entityId}' was not found.";
        }
    }
}