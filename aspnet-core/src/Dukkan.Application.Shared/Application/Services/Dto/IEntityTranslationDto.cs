namespace Dukkan.Application.Services.Dto
{
    public interface IEntityTranslationDto
    {
        string Language { get; set; }

        bool IsTranslatable();
    }
}