using Appeals.Models;

namespace Appeals.Mappers
{
    public static class Mapper
    {
        public static AppealDTO AppealsToInterface(Appeal appeal)
        {
            return new AppealDTO
            {
                Id = appeal.Id,
                Text = appeal.Text,
                Title = appeal.Title,
                CreationDate = appeal.CreationDate,
                DecisionDate = appeal.DecisionDate
            };
        }

        public static StatusDTO StatusToInterface(Status status)
        {
            return new StatusDTO
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public static TopicDTO TopicToInterface(Topic topic)
        {
            return new TopicDTO
            {
                Id = topic.Id,
                Name = topic.Name
            };
        }
    }
}
