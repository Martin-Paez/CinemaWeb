using Application.Interfaces.ICQRS.IQueries;
using Application.Interfaces.IUseCases.IShowServices;
using Domain.Entities;

namespace Application.UseCases.ShowServices
{
    public class CheckOverlappingShowsService :
        ICheckOverlappingShowsService
    {
        private readonly IShowQueries _showQueries;

        public CheckOverlappingShowsService(
            IShowQueries showQueries
            )
        {
            _showQueries = showQueries;
        }

        /// <summary>
        /// Determina si hay peliculas que se superponen en horario con una
        /// en particular.
        /// Este algoritmo sirve para comparar peliculas que tienen la misma
        /// duracion.
        /// Nota: Para los fines de la logica de negocio planteada esta
        /// solucion se considera suficiente.
        /// </summary>
        /// <param name="show">
        /// Funcion cuyo horario y dia se comparara para realizaar la busqueda.
        /// </param>
        /// <param name="duration">
        /// Duracion de las peliculas. El algoritmo trabaja con peliculas de 
        /// igual duracion.
        /// </param>
        /// <returns>
        /// Valor de verdad, donde el verdadero signfica que la sala estara
        /// ocupada en al menos una porcion de la franja horaria en cuestion.
        /// </returns>
        /// 
        public async Task<bool> IsOverlapping(
            Show show,
            int duration
            )
        {
            var hs = TimeSpan.FromMinutes(duration);
            var endTime = show.Schedule.Add(hs);
            var endDateTime = show.Date.Add(endTime);
            var startDateTime = show.Date.Add(show.Schedule);
            var shows = await GetFilteredShows(show, duration, endTime);
            var overlappings = new List<Show>();
            foreach (var s in shows)
            {
                DateTime end = s.Date.Add(s.Schedule.Add(hs));
                DateTime start = s.Date.Add(s.Schedule);
                var comparisionOne = (end.CompareTo(startDateTime) <= 0);
                var comparisionTwo = (start.CompareTo(endDateTime) >= 0);
                if (!(comparisionOne || comparisionTwo))
                    overlappings.Add(s);
            }
            return overlappings.Count > 0;
        }

        /// <summary>
        /// Cuando la funcion comienza un dia y termina en otro, es necesario
        /// comparar en dos fechas distintas. Este es un metodo auxiliar al que
        /// realiza dicho calculo. Este metodo retorna todas las funciones 
        /// que se dan el mismo dia que una en particular, para la misma sala.
        /// Este metodo permite ahorrar la cantidad de dias que se traen de
        /// la base de datos. 
        /// </summary>
        /// <param name="show">
        /// Funcion cuyo horario y dia se comparara para realizaar la busqueda
        /// </param>
        /// <param name="duration">
        /// Duracion de las peliculas. El algoritmo trabaja con peliculas de 
        /// igual duracion.
        /// </param>
        /// <param name="startTimePlusDuration">
        /// Resultado de sumar la hora de inicio de la funcion con la duracion.
        /// Al ser una funcion auxiliar, resulta conveniente recibir este 
        /// parametro que se espera halla sido calculado y utilizado antes.
        /// </param>
        /// <returns>Lista de funciones </returns>
        private async Task<List<Show>> GetFilteredShows(
            Show show,
            double duration,
            TimeSpan startTimePlusDuration
            )
        {
            var MINUTES_IN_A_DAY = 24 * 60;
            if (startTimePlusDuration.TotalMinutes > MINUTES_IN_A_DAY)
                return await _showQueries
                     .GetShowsByScreenInTheSameDayAndNextDayAs(show);
            else if (show.Schedule.TotalMinutes < duration)
                return await _showQueries
                    .GetShowsByScreenInTheSameDayAndPreviousDayAs(show);
            else
                return await _showQueries
                    .GetShowsByScreenInTheSameDayAs(show);
        }
    }
}