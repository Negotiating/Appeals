using Appeals.Models;

namespace Appeals.Mappers
{
    public static class Mapper
    {
        public static AppealDTO AppealToDTO(Appeal appeal, Status status, Topic topic)
        {
            return new AppealDTO
            {
                Id = appeal.Id,
                Title = appeal.Title,
                Text = appeal.Text,
                CreationDate = appeal.CreationDate,
                DecisionDate = appeal.DecisionDate,
                Topic = TopicToDTO(topic),
                Status = StatusToDTO(status)
            };
        }

        public static StatusDTO StatusToDTO(Status status) 
        {
            return new StatusDTO 
            { 
                Id = status.Id, 
                Name = status.Name 
            };
        }

        public static TopicDTO TopicToDTO(Topic topic)
        {
            return new TopicDTO 
            { 
                Id = topic.Id, 
                Name = topic.Name, 
                Description = topic.Description 
            };
        }

        public static Appeal ToAppeal(AppealDTO appealDTO)
        {
            return new Appeal
            {
                Id = appealDTO.Id,
                Title = appealDTO.Title,
                Text = appealDTO.Text,
                CreationDate = appealDTO.CreationDate,
                DecisionDate = appealDTO.DecisionDate,
                IdStatus = appealDTO.Status.Id,
                IdStatusNavigation = ToStatus(appealDTO.Status),
                IdTopic = appealDTO.Topic.Id,
                IdTopicNavigation = ToTopic(appealDTO.Topic)
            };
        }

        public static Status ToStatus(StatusDTO statusDTO)
        {
            return new Status
            {
                Id = statusDTO.Id,
                Name = statusDTO.Name
            };
        }

        public static Topic ToTopic(TopicDTO topicDTO)
        {
            return new Topic
            {
                Id = topicDTO.Id,
                Name = topicDTO.Name
            };
        }

    }
}
