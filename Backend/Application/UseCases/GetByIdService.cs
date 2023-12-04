using Application.Exceptions;
using Application.Interfaces.IError;
using AutoMapper;
using Domain.Entities;
using System.Text;

namespace Application.UseCases
{
    public abstract class GetByIdService<Entity>
        where Entity : SimpleIntegerIdEntity
    {
        protected readonly IMapper _mapper;
        protected readonly IErrorMessageFactory _errMsgFactory;

        protected GetByIdService(
            IMapper movieMapper,
            IErrorMessageFactory errorMessageFactory)
        {
            _mapper = movieMapper;
            _errMsgFactory = errorMessageFactory;
        }

        /// <summary>
        ///   Obtiene un Entity. Esta pensado para usarse en aquellos casos
        ///   donde se responderá un 400 (BadRequest) con la informacion de
        ///   los errores encontrados (entre los cuales se detalla el formato
        ///   incorrecto del id recibido), o bien, 404 (Not Found) si no 
        ///   existe el recurso.
        /// </summary>
        /// <param name="id"> 
        ///   Id del recurso buscado 
        /// </param>
        /// <param name="errorBuilder"> 
        ///   Mensaje al que se agregara informacion relacionada al formato
        ///   invalido del id recibido.
        /// </param>
        /// <exception cref="NoElementException">
        ///  Se lanza cuando cuando no hay un registro para el id recibido.
        /// </exception>
        /// <returns>
        ///   Retorna el recurso registrado con el id recibido.
        /// </returns>
        public async Task<Entity?> GetEntityWithInvalidIdFormatInformation(
            int id,
            StringBuilder errorBuilder
            )
        {
            Entity? entity = null;
            if (id > 0)
                entity = await GetEntityFromValidatedId(id);
            else 
                errorBuilder.Append(
                    _errMsgFactory.InvalidIdFormat<Entity>()
                );
            return entity; 
        }

        /// <summary>
        ///   Obtiene un Entity. Esta pensado para usarse en aquellos casos
        ///   en que las unicas validaciones necesarias son las que se 
        ///   realizan en este metodo.
        /// </summary>
        /// <param name="id"> 
        ///   Id del recurso buscado 
        /// </param>
        /// <exception cref="NoElementException">
        ///  Se lanza cuando cuando no hay un registro para el id recibido.
        /// </exception>
        /// <exception cref="InvalidArgumentsException">
        ///  Se lanza cuando el id tiene un formato invalido.
        /// </exception>
        /// <returns>
        ///   Retorna el recurso registrado con el id recibido.
        /// </returns>
        public async Task<Entity> GetEntityStrict(int id)
        {
            if (id < 1)
                throw new InvalidArgumentsException(
                    _errMsgFactory.InvalidIdFormat<Entity>()
                );
            return await GetEntityFromValidatedId(id);
        }

        private async Task<Entity> GetEntityFromValidatedId(int id)
        {
            var response = await GetFromRepository(id);
            if (response == null)
                throw new NoElementsException(
                    _errMsgFactory.NotFoundById<Entity>()
                );
            return response;
        }

        protected abstract Task<Entity?> GetFromRepository(int id);
    }

    public abstract class GetByIdService<Entity, EntityRespose> :
        GetByIdService<Entity>
        where Entity : SimpleIntegerIdEntity
    {
        protected GetByIdService(
            IMapper movieMapper, 
            IErrorMessageFactory errorMessageFactory
            ) 
            : base(movieMapper, errorMessageFactory)
        {
        }

        /// <summary>
        ///   Chequea la existencia de un recurso. Esta pensada para
        ///   validar foreign key.
        /// </summary>
        /// <param name="id"> 
        ///   Id del recurso buscado 
        /// </param>
        /// <param name="errorBuilder"> 
        ///   Mensaje al que se agregara informacion relacionada al formato
        ///   invalido del id o registro inexistente.
        /// </param>
        /// <returns>
        ///   Retorna True si hay un registro para el id recibido.
        /// </returns>
        public async Task<bool> Exists(
            int id,
            StringBuilder errorBuilder
            )
        {
            if (id < 1)
            { 
                errorBuilder.Append(
                    _errMsgFactory.InvalidIdFormat<Entity>()
                );
                return false;
            }
            var exists = await ExistsInRepository(id);
            if (!exists)
                errorBuilder.Append(_errMsgFactory
                    .NotFoundById<Entity>());
            return exists;
        }

        /// <summary>
        ///   Chequea la existencia de un recurso. Esta pensado para
        ///   validaciones con fluente Validation en las que la 
        ///   informacion de error se gestiona por otro medio.
        /// </summary>
        /// <param name="id"> 
        ///   Id del recurso buscado 
        /// </param>
        /// <returns>
        ///   Retorna True si hay un registro para el id recibido.
        /// </returns>
        public async Task<bool> Exists(int id)
        {
            if (id<1)
                return false;
            var a = await ExistsInRepository(id);
            return a;
        }

        public async Task<EntityRespose> GetResponseDto(int id)
        {
            Entity movie = await GetEntityStrict(id);
            return _mapper.Map<EntityRespose>(movie);
        }

        protected abstract Task<bool> ExistsInRepository(int id);
    }
}