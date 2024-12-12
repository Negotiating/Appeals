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
                //IdStatus = appeal.IdStatus,
                //IdExecutor = appeal.IdExecutor,
                //IdResident = appeal.IdResident,
                //IdPlot = appeal.IdPlot,
                //IdTopic = appeal.IdTopic,
                //Grade = appeal.Grade,
                //DeletionDate = appeal.DeletionDate,
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

            };
        }

    }
}
