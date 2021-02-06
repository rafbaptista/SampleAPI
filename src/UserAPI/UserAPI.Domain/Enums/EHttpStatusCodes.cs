using UserAPI.Domain.Attributes;

namespace UserAPI.Domain.Enums
{
    public enum EHttpStatusCodes
    {
        [MessageError("Você não tem autorização para acessar este recurso.")]
        Unauthorized = 401,

        [MessageError("Recurso não encontrado")]
        NotFound = 404,

        [MessageError("Acesso não autorizado")]
        Forbidden = 403,

        [MessageError("Tipo de mídia (payload) não suportado")]
        UnsupportedMediaType = 415,
    }
}
