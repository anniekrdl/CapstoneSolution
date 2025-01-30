namespace Core.DTOs
{

    public abstract class UserDTO
    {
        // init: 1 property kan maar 1x een waarde krijgen. en niet meer verandert worden daarna.
        public string UserName { get; init; }
        public string Role { get; init; }
        // protected: alleen toegankelijk vanuit superklasse of subklasse.
        // id op null of -1
        public virtual int? Id { get; protected set; } = null;

        public UserDTO(string userName, string role)
        {
            UserName = userName;
            Role = role;
        }


        public bool IsAdmin()
        {
            return Role.Equals("Administrator");

        }

        public int GetId()
        {
            if (Id == null)
            {
                throw new InvalidOperationException("Trying to get ID for user when ID is null!");
            }

            return Id.Value;
        }

    }


}